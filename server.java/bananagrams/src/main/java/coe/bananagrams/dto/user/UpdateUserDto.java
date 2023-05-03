package coe.bananagrams.dto.user;

import lombok.*;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class UpdateUserDto {
    public String firstName;
    public String lastName;
    public String password;
    public String username;
}
