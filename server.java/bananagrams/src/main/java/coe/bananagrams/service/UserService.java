package coe.bananagrams.service;

import coe.bananagrams.dto.user.CreateUserDto;
import coe.bananagrams.dto.user.UpdateUserDto;
import coe.bananagrams.dto.user.UserDto;
import coe.bananagrams.entity.User;
import coe.bananagrams.mapper.Mapper;
import coe.bananagrams.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class UserService {

    private final UserRepository userRepository;
    private final Mapper mapper;

    public List<UserDto> findAll() {
        List<UserDto> users = mapper.map(userRepository.findAll(), UserDto.class);
        return users;
    }

    public UserDto findById(int id) {
        UserDto user = mapper.map(userRepository.findById(id), UserDto.class);
        return user;
    }

    public void create(CreateUserDto newUserDto) {
        User user = userRepository.save(mapper.map(newUserDto, User.class));
    }

    public void update(int id, UpdateUserDto userDto) {
        User user = userRepository.findById(id);
        mapper.map(userDto, user);
        userRepository.save(user);
    }

    public void delete(int id) {
        userRepository.deleteById(id);
    }
}
