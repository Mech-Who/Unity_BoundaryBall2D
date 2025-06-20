# Game Project in Unity: Boundary Ball 2D

## 1. Introduction

This project is used to basicly learn Unity Engine.

Game rules:
1. 2 players or 1 player with an AI.
2. Each player will have a panel to control.
3. The panel can only move up and down.
4. There is a ball, will bounding in the scene (map).
5. When ball fly over any panel, the opponent will get 1 score.
6. The panel can reflect the ball and push it to the opposite direction.
7. Whoever gets the specified score first wins.

## 2. Development progress

1. Build the map. (2025-06-19)
  1. 2 panels.
  2. 1 ball.
  3. 2 score texts for ui.
  4. Up and down boundaries.
2. Make the panel control by keyboard. (2025-06-20)
  1. Add "New Script" component in Main Camera.
  2. Create variables to bind the game objects (panels, ball, texts, boundaries).
  3. Bind 'private' variables in function 'Start'.
  4. Write a function to control the specified panel with keyboard, named 'ControlPanel'.
  5. Use the 'ControlPanel' function in function 'Update'.
  6. Test the code in game.
3. Implement ball.
4. TBD

## 3. Future plan

TBD
