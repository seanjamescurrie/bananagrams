package coe.bananagrams.controller;

import coe.bananagrams.mapper.Mapper;
import coe.bananagrams.service.GameService;
import coe.bananagrams.viewModel.game.GameViewModel;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.List;

@RestController
@RequestMapping(path = "/games", produces = MediaType.APPLICATION_JSON_VALUE)
@RequiredArgsConstructor
public class GamesController {

    private final GameService gameService;
    private final Mapper mapper;

    @GetMapping()
    public ResponseEntity<List<GameViewModel>> findAll() {
        List<GameViewModel> gameViewList = mapper.map(gameService.findAll(), GameViewModel.class);
        return ResponseEntity.ok(gameViewList);
    }

    @GetMapping("/{id}")
    public ResponseEntity<GameViewModel> getById(@PathVariable int id) {
        GameViewModel gameViewModel = mapper.map(gameService.findById(id), GameViewModel.class);
        return ResponseEntity.ok(gameViewModel);
    }

    @PostMapping(consumes = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<String> create(@RequestBody String string) {
        return new ResponseEntity("Game" + string + "created", HttpStatus.CREATED);
    }

    @PutMapping(path = "/{id}", consumes = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<String> update(@PathVariable int id, @RequestBody String string) {
        return ResponseEntity.ok("Game " + id + " updated with " + string);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<String> delete(@PathVariable int id) {
        return ResponseEntity.ok("Game" + id + "deleted");
    }
}
