import maya.cmds as cmds
import colorsys as colorsys


def Control(rad, hue, sect, deg):
    objects = cmds.ls(sl=True)
    if objects:
        for item in objects:
            NewControl(item, rad, hue, sect, deg)
    else:
        NewControl(0, rad, hue, sect, deg)


def NewControl(obj, rad, hue, sect, deg):
    if deg is 0:
        deg = 360
    if sect is 0:
        sect = 30

    if obj is 0:
        newName = "Default_Ctrl"
    else:
        itemPos = cmds.getAttr(obj + ".translate")
        # cmds.move(itemPos[0], newControl, relative=True)
        newName = obj.rpartition('_')[0] + "_Ctrl"
    newControl = cmds.circle(center=(0, 0, 0), radius=rad, sections=sect, sweep=deg, name=newName, d=1)
    newControl = cmds.listRelatives(newControl)
    # newControl = cmds.rename(newControl, newName)

    cmds.setAttr(newControl + ".overrideEnabled", 1)
    cmds.setAttr(newControl + ".overrideRGBColors", 1)
    color = colorsys.hsv_to_rgb(hue / 360, 1, 0.7)
    cmds.setAttr(newControl + ".overrideColorRGB", color[0], color[1], color[2])


# Control(radius=Size, hue=1<->360 sections=NumOfSides(20+circle), sweep=0<->360)
Control(2, 282, 30, 360)
