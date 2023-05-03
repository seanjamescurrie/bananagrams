using AutoMapper;
using Bananagrams.Dal.Interfaces;
using Bananagrams.Dal.Models;
using Bananagrams.Dal.Specifications.Games;
using Bananagrams.Service.Dtos.Games;
using Bananagrams.Service.Dtos.GameTypes;
using Bananagrams.Service.HttpClients;
using Bananagrams.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace Bananagrams.Service.Services;

public class GameTypeService : IGameTypeService
{
    private readonly IBananagramsDatabase _database;
    private readonly IMapper _mapper;

    public GameTypeService(IBananagramsDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public Task<List<GameTypeDto>> GetAll() => _mapper.ProjectTo<GameTypeDto>(_database
            .Get<GameAnagramType>())
        .ToListAsync();
}