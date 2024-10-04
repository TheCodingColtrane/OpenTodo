import React, { useEffect, useState, useRef  } from 'react';
import { Modal, Box, Typography } from '@mui/material';
import { Category, Task, User } from '../../../../types';
import { backend, categoriesEndpoints, tasksEndpoints, userEndpoints } from '../../../../backend';
import styles from '../styles/Main.module.css'
import { useForm } from 'react-hook-form';
import { autocomplete, convertDateLocaleBRToDateOnly, convertDateOnlyToDateLocaleBR, maskDate, showMessage, userCredentials } from '../../../../utils';
import { TaskModalProps } from '../types';



const code = window.location.href.split('/')[4]

export const TaskModal: React.FC<TaskModalProps> = ({ open, handleClose, type, task}) => {
  const {
    register,
    setValue,
    handleSubmit,
  } = useForm<Task>();

  const onSubmit = handleSubmit(async (data) => {
    const dueDate = convertDateLocaleBRToDateOnly(data.dueDate as string)
    const selectedUser = document.querySelector("#autocomplete-field") as HTMLInputElement
    
    if(selectedUser.dataset.uc === "") {
      selectedUser.dataset.uc = userCredentials.sid
    }

    if(data.title === "" || data.title.length < 10) {
      showMessage("Campo título inválido")
      return
      // const document.querySelector('type[name="title"]')
    }

    if(!data.category){
      showMessage("Campo categoria inválido")
      return
    }

    if(!data.dueDate) {
      const date = new Date()
      const day = date.getDay()
      const month = date.getMonth()
      const year = date.getFullYear()
      data.dueDate = year +"-" + (month + 1) + "-" + day;
    }
  
    if(type === 0){
      const response = await backend.post(tasksEndpoints.base,
        {
          title: data.title,
          description: data.description,
          isCompleted: false,
          category: data.category,
          dueDate: dueDate,
          users: {code: userCredentials.sid},
          assignedUser: {code: selectedUser.dataset.uc},
          board: {code}
        })
        if(response?.ok) {
          handleClose()
          showMessage("Tarefa criada com sucesso!")

        }
    } else {
      const response = await backend.put(tasksEndpoints.base,
        {
          code: data.code,
          title: data.title,
          description: data.description,
          isCompleted: data.isCompleted,
          category: data.category,
          dueDate: dueDate,
          users: {code: userCredentials.sid},
          assignedUser: {code: selectedUser.dataset.uc},
          board: {code}
        })
        if(response?.ok) {
          showMessage("Tarefa alterada com sucesso!")
        }
    }

  
  })



  useEffect(() => {
    if (open) {
      // Fetch categories only when modal is open
      getCustomCategories();
      
      if (type === 1 && task) {
        setValue("category", task.category);
        setValue("code", task.code);
        setValue("description", task.description);
        setValue("dueDate", convertDateOnlyToDateLocaleBR(task.dueDate as string));
        setValue("isCompleted", task.isCompleted);
        setValue("title", task.title);
        setTimeout(() => {
          const assignedTo = document.querySelector("#autocomplete-field");
          if (assignedTo) {
            const assignedToField = assignedTo as HTMLInputElement;
            assignedToField.value = `${task.assignedUser?.firstName} ${task.assignedUser?.lastName}`;
          }
        }, 0);

      }
    }
  }, [open, task]);

  
const [taskCategories, setTaskCategories] = useState<Category[]>() 
const isLoadedCategories = useRef(false)
const [assignedUser, setAssignedUser] = useState<string>("")
const [users, setUsers] = useState<User[]>()
const [loadedUsers, setLoadedUsers] = useState<User[]>()
useEffect(() => {
  searchUsers(assignedUser)
}, [assignedUser])


useEffect(() => {
  if(users) autocomplete(document.querySelector("#autocomplete-field") as HTMLInputElement, users)
}, [users])

const getCustomCategories = async() => {
  
  const data = await backend.get<Category[]>(categoriesEndpoints.getBoardCategories(code))
  if(data){
    setTaskCategories(data)
    isLoadedCategories.current = true
  }
  isLoadedCategories.current = true
}


const deleteTask = async () =>{
  const response = await backend.delete<Category[]>(tasksEndpoints.getById(task!.code))
  if(response?.ok){
    showMessage('Tarefa deletada com sucesso')
  }
}


const searchUsers = async (query: string) => {
  if(query){
    const data = await backend.get<User[]>(userEndpoints.SearchUserByTerm(query))
    if(data){
      setUsers(data)
      if(loadedUsers){
        loadedUsers.push(...data)
        setLoadedUsers(loadedUsers)
        return
      } 
      setLoadedUsers(data)

    } 
  }

  return [];

}


  return (
    <Modal open={open} onClose={handleClose}>
      <Box
        sx={{
          position: 'absolute',
          top: '50%',
          left: '50%',
          transform: 'translate(-50%, -50%)',
          width: 800,
          bgcolor: 'black',
          border: '2px solid #000',
          borderRadius: '10px',
          boxShadow: 24,
          p: 4,
        }}
      >
    <Typography id="modal-modal-title" variant="h6" component="h2">
        {type == 0 ? "Crie sua tarefa" : "Altere sua tarefa"}
        </Typography>
        <Typography sx={{ mt: 2 }}>
          <form onSubmit={onSubmit}>

          <div>
            <div className={styles['input-group']}>
            <label>Título</label>
            <input type="text" id="" value={task?.title} {...register("title")} required/>
          </div>    
         
          <div className={styles['input-group']}>
          <label htmlFor="category">{type === 0 ? "Categoria" : "Trocar de categoria"}</label>
            <select {...register("category")} required>
              {
                taskCategories?.map(t => {
                  return(<option value={t.value}>{t.name}</option>)
                })
              }
            </select>
          </div>
          
            {
              type === 1 && 
              <>
            
            <input type="checkbox" name="is-completed" id="" checked/>
              </>
            }
     
          </div>
          <div className={styles['input-group']}>
            <label>Termina em</label>
            <input type="text" id="" onKeyDown={(e) => maskDate(e, e.target as HTMLInputElement)} {...register("dueDate")}/>
          </div>
          <div className={styles['input-group']}>
            <div className={styles['autocomplete']}>
            <label>Atribuir</label>
            <input type="text" id="autocomplete-field" onChange={(e) => {
              const input = e.target as HTMLInputElement
              setAssignedUser(input.value as string)
            }} data-uc={task?.assignedUser?.code} />
            </div>
          </div>   
          <label>Descrição</label>
          <textarea value={task?.description} maxLength={2048} rows={20} style={{width: "100%", padding: "10px"}} {...register("description")}></textarea>
          <div style={{ paddingTop: '5px'}}>
          <button type="submit" style={{ width: type === 0 ? '100%' : '49%', marginRight: '5px'}}>{type === 0 ? "Cadastrar" : "Alterar"}</button>
          {
            type === 1 &&
            <button type="button" onClick={() => deleteTask()} style={{backgroundColor: "#ff0000", width: '50%'}}>Deletar</button>
          }
          </div>
          </form>

        </Typography>

      </Box>
    </Modal>
  );
};

export default TaskModal;
