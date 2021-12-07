import maya.cmds as cmds  # cmds.sound(offset=10, file='ohNo.aiff')
from random import randint
import time

length = 0
memory = [0,3,2,2,3,1,0,1]
game = True
red = (0.7, 0, 0.2)
yellow = (0.9, 0.6, 0)
green = (0, 0.8, 0.2)
blue = (0, 0.4, 0.6)
white = (1, 1, 1)
b0 = 0
b1 = 0
b2 = 0
b3 = 0

def WindowMaker(size):
    global b0
    global b1
    global b2
    global b3
    window = cmds.window(title="Simon Says", iconName="icon", widthHeight=(size, size))
    cmds.rowColumnLayout(parent=window, numberOfColumns=2)
    b0 = cmds.button(l=" ", w=size / 2, h=size / 2, bgc=red, command=lambda x: Simon(0))
    b1 = cmds.button(l=" ", w=size / 2, h=size / 2, bgc=yellow, command=lambda x: Simon(1))
    b2 = cmds.button(l=" ", w=size / 2, h=size / 2, bgc=green, command=lambda x: Simon(2))
    b3 = cmds.button(l=" ", w=size / 2, h=size / 2, bgc=blue, command=lambda x: Simon(3))
    cmds.showWindow(window)


def Simon(button):
    global length
    if game:
        memory.append(randint(0, 3))
        length += 1


def ShowPattern():
    for item in memory:
        time.sleep(0.5)
        if item is 0:
            cmds.button(b0, edit=True, bgc=white)
            time.sleep(0.2)
            cmds.button(b0, edit=True, bgc=red)
        if item is 1:
            cmds.button(b1, edit=True, bgc=white)
            time.sleep(0.2)
            cmds.button(b1, edit=True, bgc=yellow)
        if item is 2:
            cmds.button(b2, edit=True, bgc=white)
            time.sleep(0.2)
            cmds.button(b2, edit=True, bgc=green)
        if item is 3:
            cmds.button(b3, edit=True, bgc=white)
            time.sleep(0.2)
            cmds.button(b3, edit=True, bgc=blue)
        print(memory[item])


WindowMaker(250)
ShowPattern()

