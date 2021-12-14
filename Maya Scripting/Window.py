import importlib
import maya.cmds as cmds
import colorsys as colorsys
import Tools
importlib.reload(Tools)


colorButton = 0


class ToolUI:
    def __init__(self):
        self.m_Window = "changeColorUIWin"
        self.width = 250
        self.height = 500
        self.color = 0
        self.radiusText = 0
        self.hueText = 0
        self.sidesText = 0
        self.sweepText = 0
        self.nameText = 0

    def Create(self):
        global colorButton
        self.Delete()  # , s=False, mxb=False
        self.m_Window = cmds.window(self.m_Window, t="RIGGER", iconName="icon", wh=(self.width, self.height))
        cmds.columnLayout()  # numberOfColumns=2
        cmds.button(l="CREATE CONTROL", c=lambda x: self.SetupControl(), bgc=(0.7, 0.7, 0.7), h=50, w=self.width, ann="Creates a control on selected objects. When nothing is selected, it will create a default one at origin.")
        cmds.rowColumnLayout(numberOfColumns=2, columnAttach=(1, 'both', 0), columnWidth=[(1, self.width / 2), (2, self.width / 2)])
        cmds.text(l='Radius', ann="The size of the control, accepts any float.")
        self.radiusText = cmds.textField(pht="Size", tx=2)
        # cmds.text(l='Hue', ann="The hue of the control, accepts any float between 0 and 360.")
        # self.hueText = cmds.textField(pht="0 - 360")
        cmds.text(l='Sides', ann="The number of sides the control has. Any num above 20 is a circle.")
        self.sidesText = cmds.textField(pht="Sides", tx=25)
        cmds.text(l='Sweep', ann="The amount of the control that is drawn, a semicircle would be 180.")
        self.sweepText = cmds.textField(pht="0 - 360", tx=360)
        cmds.text(l="\n")
        cmds.text(l="\n")
        cmds.button(l="Rename", c=lambda x: Tools.Rename('Arm_#_Jnt', 1), bgc=(0, 0.7, 0.2), h=25, w=self.width / 2, ann="Renames selected object.")
        self.nameText = cmds.textField(pht="Name")
        cmds.text(l="\n")
        cmds.text(l="\n")
        colorButton = cmds.button(l="Color Picker", c=lambda x: self.ColorPick(), bgc=(1, 1, 1), h=25, w=self.width / 2)
        cmds.button(l="Set Color", c=lambda x: Tools.ColorChange(self.color), bgc=(0.7, 0, 0.2), h=25, w=self.width / 2)
        cmds.text(l="\n")
        cmds.text(l="\n")
        cmds.showWindow(self.m_Window)

    def Delete(self):
        if cmds.window(self.m_Window, exists=True):
            cmds.deleteUI(self.m_Window)

    def ColorPick(self):
        self.color = Tools.ColorEdit()
        cmds.button(colorButton, edit=True, bgc=colorsys.hsv_to_rgb(self.color / 360, 1, 0.9))

    def SetupControl(self):
        radiusNum = float(cmds.textField(self.radiusText, q=True, tx=True))
        sidesNum = float(cmds.textField(self.sidesText, q=True, tx=True))
        sweepNum = float(cmds.textField(self.sweepText, q=True, tx=True))
        Tools.Control(radiusNum, self.color, sidesNum, sweepNum)


ToolUI().Create()
