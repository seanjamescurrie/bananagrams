package coe.bananagrams.repository;

import coe.bananagrams.entity.User;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface UserRepository extends CrudRepository<User, Integer> {
    List<User> findAll();
    User findById(int id);
    User save(User user);
    void deleteById(int id);
}
