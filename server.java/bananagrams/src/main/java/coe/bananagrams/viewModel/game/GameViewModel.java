package coe.bananagrams.viewModel.game;

import lombok.*;

import java.util.Date;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameViewModel {

    private int id;

    private String title;

    private Date dateCreated;
}
