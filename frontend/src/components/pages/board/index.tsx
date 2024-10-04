import { useEffect, useRef, useState } from 'react';
import Sidebar from './components/Sidebar';
import TaskList from './components/TaskList'
import styles from './styles/main.module.css';
import { backend, tasksEndpoints } from '../../../backend';
import { Task } from '../../../types';

function App() {
  const [tasks, setTasks] = useState<Task[]>() 
  const isLoadedTasks = useRef(false)
    
  const getTasks = async () => {
        const data = await backend.get<Task[]>(tasksEndpoints.base);
        if(data) {
            setTasks(data);
        }
   
}

useEffect(() => {
    if(!isLoadedTasks.current) getTasks()
    const body = document.getElementsByTagName('body')[0]
    body.style.display = 'initial'
    isLoadedTasks.current = true
},)


  const handleCompleteTask = (taskCode: string) => {
    setTasks(tasks?.map(task => 
      task.code === taskCode ? { ...task, completed: !task.isCompleted } : task
    ));
  };

  
  const handleTaskFilter = (tasks: Task[]) => setTasks(tasks)
  



  return (
    <div className={styles["boards-container"]}>
      <Sidebar tasks={tasks ?? []} handleTaskFilter={handleTaskFilter}/>
      {tasks &&
      <TaskList tasks={tasks} onCompleteTask={handleCompleteTask} />
      }
      <div id="snackbar"></div>
    </div>
  );
}

export default App;
