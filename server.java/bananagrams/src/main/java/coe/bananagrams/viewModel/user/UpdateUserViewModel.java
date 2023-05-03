package coe.bananagrams.viewModel.user;

import lombok.*;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class UpdateUserViewModel {
    public String firstName;
    public String lastName;
    public String password;
    public String username;
}
