package coe.bananagrams.viewModel.user;

import lombok.*;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class CreateUserViewModel {
    public String emailAddress;
    public String firstName;
    public String lastName;
    public String password;
    public String username;
}
