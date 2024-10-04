// TaskList.js
import TaskItem from './TaskItem'; // Each task
import {TaskListProps} from '../types'
import styles from '../styles/main.module.css'; // Using CSS Modules
import TaskModal from './TaskModal';
import {  useState } from 'react';


const TaskList: React.FC<TaskListProps> = ({tasks, onCompleteTask }) => { 
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true)
  const handleClose = () => setOpen(false)


     return (
      <>
    
    <div className={styles["task-list"]}>
    <div style={{paddingTop: "10px", width: "15%"}}>
      <button type="button" onClick={handleOpen}>Nova Tarefa</button>
    </div>
      <h2>Tarefas</h2>
      <ul>
        {tasks.filter(task => !task.isCompleted).map(task => (
          <TaskItem key={task.code} task={task} onComplete={onCompleteTask} />
        ))}
      </ul>
      <h2>Concluída</h2>
      <ul>
        {tasks.filter(task => task.isCompleted).map(task => (
          <TaskItem key={task.code} task={task} />
        ))}
      </ul>
    </div>
    <TaskModal
        open={open}
        handleClose={handleClose}
        type={0}
        />
      </>
   
  );
};

export default TaskList;
