package coe.bananagrams.controller;

import coe.bananagrams.dto.user.CreateUserDto;
import coe.bananagrams.dto.user.UpdateUserDto;
import coe.bananagrams.dto.user.UserDto;
import coe.bananagrams.entity.User;
import coe.bananagrams.mapper.Mapper;
import coe.bananagrams.service.UserService;
import coe.bananagrams.viewModel.user.CreateUserViewModel;
import coe.bananagrams.viewModel.user.UpdateUserViewModel;
import coe.bananagrams.viewModel.user.UserViewModel;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.List;

@RestController
@RequestMapping(path = "/users", produces = MediaType.APPLICATION_JSON_VALUE)
@RequiredArgsConstructor
public class UsersController {

    private final UserService userService;
    private final Mapper mapper;

    @GetMapping()
    public ResponseEntity<List<UserViewModel>> findAll() {
        List<UserViewModel> users = mapper.map(userService.findAll(), UserViewModel.class);
        return ResponseEntity.ok(users);
    }

    @GetMapping("/{id}")
    public ResponseEntity<UserViewModel> getById(@PathVariable int id) {
        UserViewModel userViewModel = mapper.map(userService.findById(id), UserViewModel.class);
        return ResponseEntity.ok(userViewModel);
    }

    @PostMapping(consumes = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<HttpStatus> create(@Valid @RequestBody CreateUserViewModel user) {
        CreateUserDto dto = mapper.map(user, CreateUserDto.class);
        userService.create(dto);
        return new ResponseEntity(HttpStatus.CREATED);
    }

    @PutMapping(path = "/{id}", consumes = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<HttpStatus> update(@PathVariable int id, @Valid @RequestBody UpdateUserViewModel user) {
        userService.update(id, mapper.map(user, UpdateUserDto.class));
        return new ResponseEntity(HttpStatus.OK);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<HttpStatus> delete(@PathVariable int id) {
        userService.delete(id);
        return new ResponseEntity(HttpStatus.OK);
    }
}
