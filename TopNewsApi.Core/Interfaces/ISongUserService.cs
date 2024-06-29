using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.DTO_s.User;
using TopNewsApi.Core.Services;

namespace TopNewsApi.Core.Interfaces
{
    public interface ISongUserService
    {
        Task Create(SongUserDto songsUser);
        Task Delete(SongUserDto songsUser);
        Task Update(SongUserDto songsUser);
        Task<SongUserDto?> Get(int songUserId);
        Task<List<SongUserDto>> GetAll();
        Task<List<SongUserDto>> GetAll(string userId);
        Task Delete(int songId);
        //Task<ServiceResponse> GetByName(string name);
    }
}
