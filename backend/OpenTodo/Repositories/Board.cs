using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using OpenTodo.Data;
using OpenTodo.DTOs;
using OpenTodo.Models;

namespace OpenTodo.Repositories {
    public class BoardRepository(OpenTodoContext db)
    {
        private readonly OpenTodoContext _db = db;

         private readonly BoardDTO dto = new();
          public async Task<List<BoardDTO>> GetAllBoards()
        {

            var boards = await _db.Boards.Take(30).ToListAsync();
            var boardsDTO = dto.ConvertSchemaToDTO(boards);
            return boardsDTO;

            }
        
        public async Task<List<BoardDTO>> GetAllTasksByBoardId(int id)
        {
            var boards = await _db.Boards.Where(c => c.ID == id).Take(30).ToListAsync();
             var boardsDTO = dto.ConvertSchemaToDTO(boards);
            return boardsDTO;

        }

           public async Task<List<TaskDTO>> GetTasksByTerm(string term, int boardId)
        {
            var tasks = await _db.Tasks.Where(c =>  EF.Functions.ILike(c.Title, $"%{term}%") || EF.Functions.ILike(c.Description, $"%{term}%") && c.Board.ID == boardId).Take(30).ToListAsync();
            var taskDto = new TaskDTO();
             List<TaskDTO>? taskDTO = taskDto.ConvertSchemaToDTO(tasks);
            return taskDTO;

        }

        public async Task<List<BoardDTO>> GetBoardsByUserId(int id)
        {
            var boards = await _db.BoardParticipants.Where(c => c.UserId == id).Join(_db.Boards, b => b.Boards.ID, bp => bp.ID,
            (board, participant) => new {boardID = participant.ID, name = participant.Name}).Take(30).ToListAsync();
            // var sql =  _db.BoardParticipants.Where(c => c.UserId == id).Join(_db.Boards, b => b.Boards.ID, bp => bp.ID,
            // (board, participant) => new {boardID = participant.ID, name = participant.Name}).Take(30).ToQueryString();
            List<BoardDTO> boardDTOs = [];
            foreach(var board in boards){
                boardDTOs.Add(new BoardDTO {
                    Code = new Utils.HashID().GenerateHash(board.boardID),
                    Name = board.name
                });
            }

            return boardDTOs;
       
        }

        public async Task<BoardDTO> GetBoardsById(int id)
        {
            var Board = await _db.Boards.Where(c => c.ID == id).FirstAsync();
            return dto.ConvertSchemaToDTO([Board])[0];
        }

        public async Task<int> Create(BoardSchema board){
            var currentBoard = new BoardSchema(){Name = board.Name, CreatedAt = DateTime.Now.ToUniversalTime(), UserId = board.UserId};
            var newBoard = _db.Boards.AddAsync(currentBoard);
            var savedBoard = _db.SaveChangesAsync();
            await Task.WhenAll([newBoard.AsTask(), savedBoard]);
            // var newParticipant = _db.BoardParticipants.AddAsync(new BoardParticipantSchema() {JoinedAt = new DateTime().ToUniversalTime(),
            // Boards = board, Users = new UserSchema() { Id = currentBoard.UserId }});
            var participant = await _db.BoardParticipants.OrderBy(c => c.ID).LastOrDefaultAsync();
            var now = DateTime.Now.ToUniversalTime();
            var id = participant is null ? 1 : participant.ID + 1; 
            BoardParticipantSchema boardParticipant = new(){ID = id, BoardId = currentBoard.ID, UserId = currentBoard.UserId, JoinedAt = DateTime.Now.ToUniversalTime()};
            await _db.Database.ExecuteSqlAsync($"INSERT INTO board_participant VALUES ({id}, {currentBoard.UserId}, {currentBoard.ID}, {now}, {currentBoard.UserId}, {currentBoard.ID})");
            List<CategorySchema> categorySchema =
            [
                new CategorySchema {
                    ID = 0,
                    Name = "Tarefas",
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    BoardId = currentBoard.ID
                },
                new CategorySchema {
                    ID = 0,
                    Name = "Atribuídas a mim",
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    BoardId = currentBoard.ID
                },
                 new CategorySchema {
                    ID = 0,
                    Name = "Importante",
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    BoardId = currentBoard.ID
                },
                 new CategorySchema {
                    ID = 0,
                    Name = "Meu dia",
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    BoardId = currentBoard.ID
                },
                 new CategorySchema {
                    ID = 0,
                    Name = "Planejado",
                    CreatedAt = DateTime.Now.ToUniversalTime(),
                    BoardId = currentBoard.ID
                },
            ];
            await _db.Categories.AddRangeAsync(categorySchema);
            await _db.SaveChangesAsync();

            return currentBoard.ID;

        }

        public async Task<bool> Delete(int id){
            var board = await _db.FindAsync<BoardSchema>(id);
            if(board is not null){
                _db.Boards.Remove(board);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
        
           public async Task<bool> Update(BoardSchema correctedBoard){
            var board = await _db.FindAsync<BoardSchema>(correctedBoard.ID);
            if(board is not null){
                _db.Boards.Update(correctedBoard);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    
    }

}

   

