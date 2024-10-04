import styles from '../styles/main.module.css'; // Using CSS Modules
import { backend, boardsEndpoints, categoriesEndpoints, tasksEndpoints } from '../../../../backend';
import { Category, Task } from '../../../../types';
import React, { useEffect, useRef, useState } from 'react';
import CategoriesModal from './CategoriesModal';  // Import the modal component
import { getAllCookies, userCredentials } from '../../../../utils';

// eslint-disable-next-line @typescript-eslint/no-unused-vars
const Sidebar: React.FC<{tasks: Task[],  handleTaskFilter: (task: Task[]) => void}> = ({tasks, handleTaskFilter}) => {
  const [query, setQuery] = React.useState<string>("");
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const code = window.location.href.split('/')[4]

  const [taskCategories, setTaskCategories] = useState<Category[]>() 
  const isLoadedCategories = useRef(false)
  useEffect(() => {
    if(!isLoadedCategories.current) getCustomCategories()
      isLoadedCategories.current = true
      console.log(getAllCookies())
  }, [])

useEffect(() => {
   searchTasks(query)
   
}, [query])


  const getTasksByCategory = async(categoryCode: string, boardCode: string, assignedToMe: boolean = false) => {
    let data: Task[] | null
    if(assignedToMe){
      data = await backend.get<Task[]>(tasksEndpoints.getByUser(boardCode))
    } else {
      data = await backend.get<Task[]>(tasksEndpoints.getTasksByCategory(boardCode, categoryCode,  ""))

    }
    if(data) handleTaskFilter(data)
  }

  const getCustomCategories = async() => {
    const data = await backend.get<Category[]>(categoriesEndpoints.getBoardCategories(code))
    if(data) setTaskCategories(data)

  }



  const handleNewCategory = (newCategory: Category) => {
    taskCategories?.push(newCategory)
    setTaskCategories(taskCategories);
  };

  const searchTasks = async (query: string) => {
    if(query){
      const data = await backend.get<Task[]>(boardsEndpoints.searchTasksOnBoard(code, query))
      if(data){
        handleTaskFilter(data)
      } 
    }

  }

  return (
    <aside className={styles.sidebar}>
      <div className={styles["profile-section"]}>
        {/* <img src="profile-pic-url" alt="Profile" /> */}
        <h4>{userCredentials.name}</h4>
        <form>
        <input type="search" name="" id={styles["search-task-input"]} placeholder='Pesquisar' onChange={(e) => {
          const input = e.target as HTMLInputElement
          setQuery(input.value)
          }}/>
        </form>
      </div>
      <ul className={styles["sidebar-menu"]}>
        {taskCategories?.map(category => (
        <li data-id={category.code} key={category.code} onClick={(e) => {
          const currentItem = e.target as HTMLElement
          if(currentItem.innerHTML === "Atribuídas a mim"){
            getTasksByCategory(category.code, code, true)
          } else {
            getTasksByCategory(category.code, code)
          }
          }}>{category.name
          }</li>
      ))}
      </ul>
      <div style={{paddingTop: '10px', width: '20%'}}>
      <button type='button' onClick={handleOpen}>Nova Lista</button>

      </div>
      <CategoriesModal
        open={open}
        handleClose={handleClose}
        handleCategories={handleNewCategory}
        />
    </aside>
  );
};

export default Sidebar;
