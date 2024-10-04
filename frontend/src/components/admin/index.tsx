import { useEffect, useState } from 'react';
import '../../App.css';
import { Task  } from '../../types';
import { backend, tasksEndpoints } from '../../backend'

function Board() {
    const [tasks, setTasks] = useState<Task[]>();

    useEffect(() => {
        populateWeatherData();
    }, []);

    const contents = tasks === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>    
            <tbody>
                {tasks.map(task =>
                    <tr key={task.code}>
                        <td>{task.title}</td>
                        <td>{task.description}</td>
                        <td>{task.category}</td>
                        <td>{task.isCompleted}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateWeatherData() {
        const data = await backend.get<Task[]>(tasksEndpoints.base);
        setTasks(data ? data : []);
    }
}

export default Board;