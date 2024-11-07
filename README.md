# City Player - Trash Collection Branch

City Player is an interactive simulation designed to create a dynamic downtown walking experience. Users can adjust environmental features in real time, such as greenery, building height, traffic, and more, to create their ideal urban setting.

---

## Project Overview

City Player allows users to:
- Explore a realistic downtown environment in first-person.
- Customize settings for trees, buildings, traffic density, and public spaces.
- Experience the effect of urban elements on a leisurely and utilitarian walk.

---

## Getting Started

1. **Clone the Repository**: Download the project files from your repository or local drive.

2. **Open in Unity**:
   - Recommended Unity version: **2021.3 or newer**.
   - Open the project in Unity and load the main scene (`MainScene`).

3. **Customize Your Walk**:
   - Use in-game settings to adjust:
     - Greenery (trees, bushes, flowers).
     - Building heights and spacing.
     - Traffic density and pedestrian flow.
     - Amenities like public seating and social spaces.
   - Real-time changes will immediately affect the environment, allowing for a tailored experience.

---

## Key Features

- **Real-Time Environmental Customization**:
  - Adjust trees, greenery, and building heights to influence the aesthetics and functionality of your walk.
  - Change traffic density and pedestrian movement to simulate different levels of urban activity.

- **Leisure vs. Utilitarian Walk Modes**:
  - Toggle between two walking modes:
    - **Leisure Mode**: Prioritize visual beauty, greenery, and social spaces.
    - **Utilitarian Mode**: Focus on efficiency, with amenities and transport infrastructure being key factors.

- **Dynamic Time-of-Day Simulation**:
  - Control the time of day (from 9 AM to 6 PM) to observe how shadows and lighting impact the walk.

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
