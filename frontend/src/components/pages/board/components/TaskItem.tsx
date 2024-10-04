import React, { useEffect, useState } from 'react';
import { TaskItemProps } from '../types';
import styles from '../styles/main.module.css';
import { Task } from '../../../../types';
import TaskModal from './TaskModal';  // Import the modal component
import { useForm } from 'react-hook-form';
import { backend, tasksEndpoints } from '../../../../backend';
import { showMessage } from '../../../../utils';

const TaskItem: React.FC<TaskItemProps> = ({ task, onComplete }) => {
  const [open, setOpen] = useState(false);

  const handleOpen = () => setOpen(true)
  const handleClose = () => setOpen(false)

  const {
    register,
    setValue,
  } = useForm<Task>();

useEffect(() => {
  setValue("isCompleted", task.isCompleted)
}, [])

const updateTaskStatus = async (task: Task) => {
  const listItem = document.querySelector(`[data-code=${task.code}]`) as HTMLElement
  const chkBox = document.querySelector(`[data-code=${task.code}]`) as HTMLInputElement
  const response = await backend.put(tasksEndpoints.base,
    {
      code: task.code,
      title: task.title,
      description: task.description,
      isCompleted: chkBox.value === "on" ? true : false,
      category: task.category,
      dueDate: task.dueDate,
      users: {},
      assignedUser: {},
      board: {code: window.location.href.split('/')[4]}
    })
    if(response?.ok) {
      showMessage("Tarefa alterada com sucesso!")
      listItem.className =styles["task-item-completed"]
    }
}



  const renderItem = (task: Task) => {
    if (task.isCompleted) {
      return (
        <li key={task.code}
        data-code={task.code}
          className={styles['task-item-completed']} 
          style={{ padding: '5px', cursor: 'pointer' }} 
          onClick={(e) => {
            const element = e.target as HTMLElement
            if(element.tagName !== 'INPUT') handleOpen()
          }}
        >
          <input
            type="checkbox"
            {...register("isCompleted")}
            onChange={(e) => {
              e.stopPropagation();  // Prevent modal from opening on checkbox click
              onComplete!(task.code);
              updateTaskStatus(task)
            }}
          />
          <span style={{ padding: '5px' }}>{task.title}</span>
          <div>
            <span className={styles['due-date']}>
              {new Date(task.dueDate ).toLocaleDateString()}
            </span>
          </div>
        </li>
      );
    }

    return (
      <li key={task.code}
      data-code={task.code}
        className={styles['task-item']} 
        style={{ padding: '5px', cursor: 'pointer' }} 
        onClick={(e) => {
          const element = e.target as HTMLElement
          if(element.tagName !== 'INPUT') handleOpen()
        }}
        >
        <input
          type="checkbox"
          {...register("isCompleted")}
          onChange={(e) => {
            e.stopPropagation();
            onComplete!(task.code);
            updateTaskStatus(task)
          }}
        />
        <span style={{ padding: '5px' }}>{task.title}</span>
        <div>
          <span className={styles['due-date']}>
            {new Date(task.dueDate).toLocaleDateString()}
          </span>
        </div>
      </li>
    );
  };

  return (
    <>
    
      {renderItem(task)}

      <TaskModal
        open={open}
        handleClose={handleClose}
        type={1}
        task={task}
        />
    </>
  );
};

export default TaskItem;
