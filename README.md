# LP2 Project 3

## Blue Light

### Authors

#### Pedro Inácio - a21802050 
[PmaiWoW](https://github.com/PmaiWoW)

#### Catarina Matias - a21801693 
[StarryNight00](https://github.com/StarryNight00)

#### Afonso Rosa - a21801515 (Not enrolled in this class)

### Tasks of each group member

#### Pedro Inácio 

##### NPC, dialogue classes, karma and UI, level design, general quality checking, XML commenting, Doxygen generation, UML creation and README redaction;

Main creator of the dialogue and karma systems and responsible for their
incorporation and UI throughout the project. Participated in level design.
Creator and/or main contributor to the `Dialogue`, `NPC`, `DialogueManager`,
`Karma` and enum `Faction`. Responsible for quality checking all of the code
made and attempting to improve it where and when possible.

Responsible for all of the XML commenting, generation of Doxygen documentation
and UML creation, working in tandom with Catarina Matias to write the
`README.md` file.

#### Catarina Matias

##### TicketTimer, Camera & Player Movement, in-Engine Animations, player-UI canvas, level design;

Responsible for the `TicketTimer` and `Player` classes. Was responsible for the
incorporation of the code given in DJD classes in this project and it's
adaptation to the current project. Regular tasks included level design
and code updating, along with code support to the team and fixes.

Work especially focussed on the "visual" component, such as creation of
assets, like the crosshair and its associated code, and the visual
language used throughout in the 3D models and materials.

### Project's Git Repository

[Blue Light Repository](https://github.com/StarryNight00/DJD2BlueLight/tree/master)

### Describing Solution approach

The Blue Light Project is a project that started at the DJD2 classes, with the
idea of creating a walking simulator with 'conflict' in mind. Our project
focused on the thought conflict in a society, and two opposing extremes of
reality.

When transposing these concepts to Unity, a priority was the dialogue
system - developed by Pedro - as it allowed for the player to experience
our idea and be introduced more and more to an imperfect and extreme world.
Meanwhile, Catarina and Afonso focused on the script and level design to
incorporate the dialogue on.

As for the visual component, Catarina was focused more here, making the
visual assets and the User Interface, on the `Canvas` class.

Afonso focussed on the level design and programmed the `Menu` classes, along
with their scenes. When pressing `ESC`, the player can exit to either the
Start Menu and restart the game, or leave for the Desktop.

During the game, as the player moves through the room, they have to talk to
the others and collect items. The other members will introduce the ideologies
to them, or put the Player on a path to find a secret ending and escape the
interview.

Scriptable Objects were used by Pedro to facilitate adding dialogue to NPCs, as
a way of allowing interactions between the player and the NPCs to be created
with no code necessary, as to allow Afonso to simply drop a Dialogue into the
NPC's appropriate field in the editor and fill out the conversation as they
pleased, allowing for choices to be made as well.

A rudimentary karma system for the player was also developed by Pedro, but the
end goal we hoped to achieve with it in the game was, unfortunately, not
achieve, although the system present allows for it to be completed at a later
date.

We did not start this project with a specific pattern in mind. We can say we
used the patterns that exist naturally within the Unity code, such has the
`Update Pattern`. However, no specific pattern was used consciously.

### UML Diagram

The following class diagram represents associations between classes. The are
more Dependency arrows but to make the diagram readable only the most important
arrows are available.

![UML_Diagram](UML_Diagram.png)

## References

The following references where used during this project.

**[1]** Linguagens de Programação 2 class Power-points.

**[2]** Our classmates Leandro Brás and André Vitorino helped with some concept
and basic logic for the dialog system, giving tips and pointers on how the
logic could be done.

**[3]** Desenvolvimento de Jogos Digitais classes. The code developed in these
classes was used as a stepping stone for most of the code that was common to
the ideas presented.

**[4]** Unity and Microsoft's C# APIs.

**[5]** Reference books of the Linguagens de Programação 2 class were consulted
many times throughout this project.
