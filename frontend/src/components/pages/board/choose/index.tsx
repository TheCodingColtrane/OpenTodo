import { useEffect, useRef, useState } from 'react';
import { backend,  boardsEndpoints} from '../../../../backend';
import { Board } from '../../../../types';
import BoardModal from './BoardModal';
import { NewBoardResponse } from './types';

function Choose() {
  const [boards, setBoards] = useState<Board[]>()
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const isLoadedBoards = useRef(false)
    
  const getBoards = async () => {
        const data = await backend.get<Board[]>(boardsEndpoints.getUserBoards());
        if(data) {
            setBoards(data);
        }
   
}

useEffect(() => {
    if(!isLoadedBoards.current) getBoards()
    const body = document.getElementsByTagName('body')[0]
    body.style.display = 'initial'
    isLoadedBoards.current = true
},)

const createBoard = async () => {
    const name = document.querySelector('#board-name') as HTMLInputElement
    if(name.value){ 
    const response = await backend.post(boardsEndpoints.base, {id: 0, name: name.value,
        createdAt: new Date(), updatedAt: new Date(), user: {id: 0, firstName: "", lastName: "", dob: "2024-12-31",
            username: "", passwordHash: "", createdAt: new Date()
        }})
        if(response?.ok) {
            const data = await response.json() as NewBoardResponse
            window.location.href = "/board/" + data.code
        } 
    }
  }  
  

const renderBoards = () => {
    if(boards){
        return (
            <>
            <h4 style={{textAlign: 'center'}}>Selecione seu quadro</h4>
            <div style={{padding: '15px', display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: '20%', borderRadius: "5px", border: "1px solid white" }}>
                  {boards?.map((board, i) => (
               <a href={`/board/${board.code}`}><li key={i} style={{  listStyleType: "none", color: 'white'}}>{board.name}</li></a>
              ))}
            </div>
            <button type="button" onClick={() => handleOpen}>Criar Quadro</button>
          
            
            </>
          );
    }

    return (

        // <div style={{display: 'flex', alignItems: 'center', padding: '15px', position: 'absolute', left: '50%', height: '50%', width: '50%'}}>
        <div style={{padding: '10px', display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: '20%' } }>
            <h4 style={{padding: "1%"}}>Crie seu quadro</h4>
            {/* <label htmlFor="category">Nome da lista</label> */}
            <input type="text" minLength={3} maxLength={50} style={{width: '60%', padding: '7px'}} placeholder='Nome da quadro' id='board-name'/>
            <button type="button" style={{marginTop: "10px", width: '60%'}} onClick={createBoard}>Criar</button>
            </div>       
            //  </div>
    )
}

  return (
    <>
        {renderBoards()}
       <BoardModal
        open={open}
        handleClose={handleClose}
        />
    </>

  )
  
}

export default Choose;
