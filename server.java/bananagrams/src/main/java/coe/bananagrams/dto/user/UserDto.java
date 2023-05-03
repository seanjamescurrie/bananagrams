package coe.bananagrams.dto.user;

import coe.bananagrams.dto.gameUser.GameUserDto;
import lombok.*;

import java.util.Date;
import java.util.List;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class UserDto {
    private int id;
    private Date dateCreated;
    private String firstName;
    private String lastName;
    private String username;
}
