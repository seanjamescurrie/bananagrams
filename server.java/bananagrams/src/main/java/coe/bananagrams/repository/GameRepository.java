package coe.bananagrams.repository;

import coe.bananagrams.entity.Game;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface GameRepository extends CrudRepository<Game, Integer> {

    List<Game> findAll();
    Game findById(int id);

    Game save(Game game);

    void deleteById(int id);
}
