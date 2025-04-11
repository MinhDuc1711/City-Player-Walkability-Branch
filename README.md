# City Player - Walkability Branch

City Player is an interactive simulation that offers a dynamic downtown walking experience. Participants can customize environmental elements in real time—such as greenery, building diversity, and public spaces—to shape their ideal urban streetscape. Users who walk through the environment can evaluate and rank key street features based on their importance to the walking experience. The data collected supports urban planners and researchers in designing more livable and people-centric cities.

---
## Link to Demo

https://drive.google.com/file/d/1JaT_hWjZBcarpb0pvKWob2Ir0BPRek8p/view 

## Top Files

| File path with clickable Github link | Purpose |
| :---         |     :---:      |  
| [Building Dictionary List script](https://github.com/MinhDuc1711/City-Player-Walkability-Branch/blob/main/Assets/Scripts/Buildings/BuildingDictionary2ListAttempt.cs)     | Controls the variety of buildings along the street based on the slider value.    |
| [Street Connectivity Script](https://github.com/MinhDuc1711/City-Player-Walkability-Branch/blob/main/Assets/Scripts/Connectivity/ConnectivitySlider.cs)   |Controls the ability to generate intersections on the street by adjusting the Street Connectivity Slider.| 
| [UI Menue Manager script](https://github.com/MinhDuc1711/City-Player-Walkability-Branch/blob/main/Assets/Scripts/UI/UI%20Manager/uiMenuManager.cs)       | Control and manage the ui menus so that there are never 2 UIs at the same time. | 
| [Greenery Generator script](https://github.com/MinhDuc1711/City-Player-Walkability-Branch/blob/main/Assets/Scripts/Greenery/GreeneryGeneration.cs)   | Generates the greenery with the selection of assets of trees and flower pots | 
| [Waypoint Navigator script](https://github.com/MinhDuc1711/City-Player-Walkability-Branch/blob/main/Assets/Scripts/Traffic/WaypointNavigator.cs)  | Allows the traffic system to follow the waypoints | 


## Top Test Files

| Test file path with clickable Github link | Purpose |
| :---         |     :---:      |  
| https://github.com/MinhDuc1711/City-Player-Walkability-Branch/blob/main/Assets/Tests/CharacterNavigationControllerTests.cs | Test Character Navigation Controller |
| https://github.com/MinhDuc1711/City-Player-Walkability-Branch/blob/main/Assets/Tests/WaypointTests.cs | Test Waypoints in Traffic System | 
| https://github.com/MinhDuc1711/City-Player-Walkability-Branch/blob/main/Assets/Tests/BuildingDictionary2ListAttemptTest.cs | Test Change in building density  | 
| https://github.com/MinhDuc1711/City-Player-Walkability-Branch/blob/main/Assets/Tests/GreenerySliderTests.cs | Test Greenery generation and deletion  | 
| https://github.com/MinhDuc1711/City-Player-Walkability-Branch/blob/main/Assets/Tests/PedestrianSpawnerTests.cs | Test Spawning of Pedestrians on Street | 

## Project Overview

City Player allows users to:
- Explore a realistic downtown environment in first-person.
- Customize greenery, buildings, connectivity, public spaces, and enclosure settings.
- Experience enhanced realism from ambient pedestrian activity on sidewalks and traffic, as the user navigates the street.
- Rank the importance of individual street features by assigning points, providing valuable data for urban planning analysis.

---

## Getting Started

1. **Clone the Repository**: Download the project files from your repository or local drive.

2. **Open in Unity**:
   - Recommended Unity version: **2021.3 or newer**.
   - Open the project in Unity and load the main scene (`MainScene`).

3. **Customize Your Walk**:
   - Use in-game settings to adjust:
     - Greenery (trees, bushes, flowers).
     - Building diversity.
     - Connectivity (Block length).
     - Enclosure
     - Amenities like public seating and social spaces.
   - Real-time changes will immediately affect the environment, allowing for a tailored experience.

---

## Key Features

- **Real-Time Environmental Customization**:
  - Adjust greenery, building diversity, and public areas to influence the aesthetics and functionality of your walk.
  - Change the block length and the enclosure to model a different overall look for the city.

- **Shading**:
  - Realistic shading techniques to enhance visual depth and improve the overall immersion of the street environment.
    
- **Player Feedback**:
  - At the end of the experience, users complete a short survey by assigning a total of 100 points to different street features based on their importance.


---


## Assets and Dependencies

- **Imported Assets**: Key assets are organized under `Assets/ImportedAssets/`. Some main assets:
  - **Intelligent Traffic System (ITS)**: Realistic traffic flow and control.
  - **Greenery Assets**: Detailed tree and bush models for visual beauty.
  - **Building Materials and Textures**: Archtoolkit plugin for building materials (located at `Assets/Ambiens/Archtoolkit/AT+Materials`).

---

## Technical Notes

- **First-Person Camera**: The simulation uses Unity’s Starter Assets package, which is located in `Assets/ImportedAssets/StarterAssets`, for easy navigation.
- **Plugin Usage**: Archtoolkit is used for materials, enabling detailed browsing and selection of materials. This can be accessed through Unity’s asset panel.
  
---

### Tools Used

![Unity](https://upload.wikimedia.org/wikipedia/commons/1/19/Unity_Technologies_logo.svg)  
**Unity 6** – Used as the primary game engine to build the simulation.

---

## Contributors

| Name | Student ID | Github |
| :---         |     :---:      |          ---: |
| Luca Dallaire          | 40132255  | lucadallaire |
| Cristian Gasparesc     | 40209205  | CritixGames |
| Amirhossein Tavakkoly  | 40203604  | amirhossein942 |
| Alexandre Careau       | 21336665  | acareau |
| Jason Novio            | 40154259  | Jason-817 |
| Daniel Bondar          | 40213095  | KiwamiMeansExtreme |
| Ryan Sefrioui          | 40157471  | Ryan30012 |
| Cosmin Suna            | 40125921  | Coscos100 |
| Minh Duc Vu            | 40166824  | MinhDuc1711 |
| Jackson Jean           | 23507416  | jacksnj |
| Jose Semaan            | 40244141  | jozesemaan |

