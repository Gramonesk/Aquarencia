<h1>Aquarencia</h1>

![0708](https://github.com/Gramonesk/Aquarencia/assets/154248035/a1bfb4f8-25b0-4f2d-b076-3702cf897b18)

## About This Project
  This project was developed as a serious game to face harming behaviour towards the environment specifically for sea turtles, hence the purpose : to increase awareness on people regarding harm towards sea turtles. The game itself uses some unique mechanics that was customly made in which are 
* Capturing and Saving photos
* Selling the photos
* Organizing the shop and also the social media

  
| **Role** | **Team Size** | **Development Time** | **Engine** |
|:---:|:---:|:---:|:---:|
| Game Programmer | 3 | 3 Weeks | Unity 2022.3.5f1 |

My role : Game Programmer (includes post processing, movement, menu)

## About Game
<u><b>Aquarencia</b></u> is an <i>Adventure and Role-Playing Game (RPG)</i> where players take on the role of the main character, <u><b>Made</b></u>. Made explores an underwater world and interacts with turtles using a camera and his submarine. Interestingly, Made also serves as an influencer and shopkeeper, managing social media accounts and the shop itself. The game combines exploration, photography, and social simulation elements.

## High Concept Statement
A wildlife photographer, drawn by childhood memories of the underwater world, receives a plea for help from a close friend who has secured a turtle conservation license. Eager to contribute, the photographer leverages their influencer status to boost both awareness and funding for their friend’s cause. Players step into the photographer’s shoes, embarking on an adventure that unfolds through their lens.

## Mechanics I utilize
1. Data Persistence
   - using JSON, filestream and furthermore using generics and interfaces to make it modular and appliable for all my other projects, this mechanics allows me to save data ex: string datas, pictures and more
   - using inventory system that retrieves data i saved either by singleton referencing or straight from loading the game so that the photo data can be used to sell and display what was taken before 
1. Screen snapping and game resolution
   - used for taking photos of the sea turtles and saving it, this also scales with the game resolution so that it wont break the game
1. Design Patterns
   - using an Invoker so gameplay feels robust especially when interacting with the pause menu or UI
1. Navigation mesh
   - using a navmesh to make the npc move and interact with the environment to make the gameplay feel more filled.
1. URP POST-PROCESSING
    -  Implimentation of post-processing effects in unity
    -  Lights 2D used for improved visual
1. Object pooling
   - using an object pooling to reduce memory buffer and also a large performance boost on the game
  
## What i learned

## Controls
Main Input:
|**KeyCode**|**Function**|
|:---:|:---:|
| Escape | (Open pause menu / Exit UI)|

Top Down Movement:
|**KeyCode**|**Function**|
|:---:|:---:|
| W | Up (Movement)|
| A | Left (Movement)|
| S | Down (Movement)|
| D | Right (Movement)|
| Space | Interact |

UI Interaction:
|**KeyCode**|**Function**|
|:---:|:---:|
| LMB | Interact |
| Escape | Cancel / Exit |

Egg Hatching gameplay:
|**KeyCode**|**Function**|
|:---:|:---:|
| LMB | drag egg / turtle|
| release LMB| drop egg / turtle|

Turtle Feeding gameplay:
|**KeyCode**|**Function**|
|:---:|:---:|
|WASD| Input to feed food |

Submarine gameplay:
|**KeyCode**|**Function**|
|:---:|:---:|
|Space| Toggle View |
|Mouse input|Move camera view|
|Scrollwheel | Zoom View|
|LMB| Take Photo|
