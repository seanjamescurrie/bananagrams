package coe.bananagrams.dto.user;

import jakarta.persistence.Entity;
import lombok.*;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class CreateUserDto {
    private String emailAddress;
    private String firstName;
    private String lastName;
    private String password;
    private String username;
}
