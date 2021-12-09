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
        self.radiusText = 0
        self.hueText = 0
        self.sidesText = 0
        self.sweepText = 0
        self.radiusNum = 0
        self.hueNum = 0
        self.sidesNum = 0
        self.sweepNum = 0

    def Create(self):
        global colorButton
        self.Delete()  # , s=False, mxb=False
        self.m_Window = cmds.window(self.m_Window, t="RIGGER", iconName="icon", wh=(self.width, self.height))
        cmds.columnLayout(p=self.m_Window)  # numberOfColumns=2
        cmds.button(l="CREATE CONTROL", c=lambda x: Tools.Control(float(self.radiusNum), float(self.hueNum), float(self.sidesNum), float(self.sweepNum)), bgc=(0.7, 0.7, 0.7), h=50, w=self.width, ann="Creates a control on selected objects. When nothing is selected, it will create a default one at origin.")
        cmds.rowColumnLayout(numberOfColumns=2, columnAttach=(1, 'both', 0), columnWidth=[(1, self.width / 2), (2, self.width / 2)])
        cmds.text(l='Radius', ann="The size of the control, accepts any float.")
        radiusTxt = cmds.textField(pht="Size")
        cmds.text(l='Hue', ann="The hue of the control, accepts any float between 0 and 360.")
        hueTxt = cmds.textField(pht="0 - 360")
        cmds.text(l='Sides', ann="The number of sides the control has. Any num above 20 is a circle.")
        sidesTxt = cmds.textField(pht="Sides")
        cmds.text(l='Sweep', ann="The amount of the control that is drawn, a semicircle would be 180.")
        sweepTxt = cmds.textField(pht="0 - 360")
        m_column2 = cmds.rowColumnLayout(p=self.m_Window, numberOfColumns=2)
        colorButton = cmds.button(p=m_column2, l="Color Picker", c=lambda x: self.ColorPick(), bgc=(1, 1, 1), h=50, w=self.width / 2)
        cmds.button(p=m_column2, l="Rename", c=lambda x: Tools.Rename('Arm_#_Jnt', 1), bgc=(0, 0.7, 0.2), h=50, w=self.width / 2)
        cmds.button(p=m_column2, l="Set Color", c=lambda x: Tools.ColorChange(color), bgc=(0.7, 0, 0.2), h=50, w=self.width / 2)
        cmds.showWindow(self.m_Window)

    def Delete(self):
        if cmds.window(self.m_Window, exists=True):
            cmds.deleteUI(self.m_Window)

    def ColorPick(self):
        global color
        color = Tools.ColorEdit()
        cmds.button(colorButton, edit=True, bgc=colorsys.hsv_to_rgb(color / 360, 1, 0.9))

    def TextBoxes(self):
        radiusNum = cmds.textField(self.radiusText, q=True, tx=True)
        hueNum = cmds.textField(self.hueText, q=True, tx=True)
        sidesNum = cmds.textField(self.sidesText, q=True, tx=True)
        sweepNum = cmds.textField(self.sweepText, q=True, tx=True)
        return


ToolUI().Create()
