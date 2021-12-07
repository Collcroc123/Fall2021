import importlib
import maya.cmds as cmds
import colorsys as colorsys
import Tools
importlib.reload(Tools)


colorButton = 0
color = 0


class ToolUI:
    def __init__(self):
        self.m_Window = "changeColorUIWin"
        self.width = 250
        self.height = 500

    def Create(self):
        global colorButton
        self.Delete()  # , s=False, mxb=False
        values = cmds.colorEditor(query=True, hsv=True)
        self.m_Window = cmds.window(self.m_Window, t="RIGGER", iconName="icon", wh=(self.width, self.height))
        m_column = cmds.rowColumnLayout(p=self.m_Window, numberOfColumns=2)  # numberOfColumns=2
        cmds.button(l="Create Sphere", c="cmds.polySphere()", bgc=(0.5, 0, 0.7), h=50, w=self.width / 2)
        colorButton = cmds.button(p=m_column, l="Color Picker", c=lambda x: self.ColorPick(), bgc=(1, 1, 1), h=50, w=self.width / 2)
        cmds.button(p=m_column, l="Rename", c=lambda x: Tools.Rename('Arm_#_Jnt', 1), bgc=(0, 0.7, 0.2), h=50, w=self.width / 2)
        cmds.button(p=m_column, l="Set Color", c=lambda x: Tools.ColorChange(color), bgc=(0.7, 0, 0.2), h=50, w=self.width / 2)
        cmds.showWindow(self.m_Window)

    def Delete(self):
        if cmds.window(self.m_Window, exists=True):
            cmds.deleteUI(self.m_Window)

    def ColorPick(self):
        global color
        color = Tools.ColorEdit()
        cmds.button(colorButton, edit=True, bgc=colorsys.hsv_to_rgb(color / 250, 1, 0.9))


ToolUI().Create()
