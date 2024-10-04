using OpenTodo.DTOs;
using OpenTodo.Models;
using OpenTodo.Repositories;
using OpenTodo.Utils;

namespace OpenTodo.Services
{
    public class BoardService(BoardRepository boardRepo)
    {
        private readonly BoardRepository _boardRepo = boardRepo;
        private readonly HashID hashID = new();

        public async Task<List<BoardDTO>> GetAllBoards()
        {
            return await _boardRepo.GetAllBoards();

        }

        public async Task<List<BoardDTO>> GetAllTasksByBoardId(string code)
        {
            int id = hashID.ReverseHash(code);
            if(id == 0) return [];
            return await _boardRepo.GetAllTasksByBoardId(id);
        }

        public async Task<BoardDTO> GetBoardsById(string code)
        {
            int id = hashID.ReverseHash(code);
            if(id == 0) return new();
            return await _boardRepo.GetBoardsById(id);
        }

          public async Task<List<BoardDTO>> GetBoardsByUserId(int id)
        {
            return await _boardRepo.GetBoardsByUserId(id);
        }

        public async Task<List<TaskDTO>> SearchTasksByTerm(string code, string query)
        {
             int id = hashID.ReverseHash(code);
            if(id == 0) return [];
            return await _boardRepo.GetTasksByTerm(query, id);
        }

        public async Task<string> Create(BoardSchema board)
        {
            var id = await _boardRepo.Create(board);
            if(id > 0){
                return hashID.GenerateHash(id);
                
            }

            return "";
        }

        public async Task<bool> Update(BoardDTO board)
        {
            int id = hashID.ReverseHash(board.Code);
            if(id == 0) return false;
            BoardSchema boardToUpdate = new() {ID = id, Name = board.Name, UserId = hashID.ReverseHash(board.User.Code)};
            return await _boardRepo.Update(boardToUpdate);
        }
        
        public async Task<bool> Delete(string code)
        {
            int id = hashID.ReverseHash(code);
            if(id == 0) return new();
            return await _boardRepo.Delete(id);
        }
    }
}
