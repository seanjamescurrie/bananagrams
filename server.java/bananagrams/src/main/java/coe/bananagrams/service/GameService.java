package coe.bananagrams.service;

import coe.bananagrams.dto.game.GameDto;
import coe.bananagrams.mapper.Mapper;
import coe.bananagrams.repository.GameRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class GameService {

    private final GameRepository gameRepository;
    private final Mapper mapper;

 public List<GameDto> findAll() {
     List<GameDto> gameDtoList = mapper.map(gameRepository.findAll(), GameDto.class);
     return gameDtoList;
 }

 public GameDto findById(int id) {
     GameDto gameDto = mapper.map(gameRepository.findById(id), GameDto.class);
     return gameDto;
 }
}
