# Simple Turtle Game
# Instructions: Move the turtle with arrow keys and collect shapes.

import turtle
import random

# Set up the screen
screen = turtle.Screen()
screen.title("Turtle Game")
screen.bgcolor("white")

# Create the player turtle
player = turtle.Turtle()
player.shape("turtle")
player.color("blue")
player.penup()
player.speed(0)  # Set the turtle's animation speed to the maximum

# Create a shape turtle
shape = turtle.Turtle()
shape.shape("circle")
shape.color("red")
shape.penup()
shape.speed(0)
shape.goto(random.randint(-200, 200), random.randint(-200, 200))

# Function to move the player turtle
def move_up():
    y = player.ycor()
    y += 20
    player.sety(y)

def move_down():
    y = player.ycor()
    y -= 20
    player.sety(y)

def move_left():
    x = player.xcor()
    x -= 20
    player.setx(x)

def move_right():
    x = player.xcor()
    x += 20
    player.setx(x)

# Keyboard bindings
screen.listen()
screen.onkey(move_up, "Up")
screen.onkey(move_down, "Down")
screen.onkey(move_left, "Left")
screen.onkey(move_right, "Right")

# Function to check for collisions
def check_collision():
    if player.distance(shape) < 20:
        shape.goto(random.randint(-200, 200), random.randint(-200, 200))

# Main game loop
while True:
    check_collision()
    screen.update()  # Update the screen

# Uncomment the following line if you are not running this in IDLE
# screen.mainloop()
