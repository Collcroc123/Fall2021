import maya.cmds as cmds


#import ToolUI
#reload ToolUI

class ToolUI:
    def __init__(self):
        self.m_Window = "changeColorUIWin"
        self.width = 250
        self.height = 500

    def create(self):
        self.delete()
        self.m_Window = cmds.window(self.m_Window, title="RIGGER", iconName="icon", widthHeight=(self.width, self.height))
        m_column = cmds.columnLayout(parent=self.m_Window, adjustableColumn=True)  # numberOfColumns=2
        cmds.button(parent=m_column, label="Sphere", command="cmds.polySphere()", backgroundColor=(0.7, 0, 0.6))
        cmds.button(parent=m_column, label="Color", command=lambda x: ChangeColor(13), backgroundColor=(0.7, 0, 0.2))
        cmds.button(parent=m_column, label=" ")
        cmds.showWindow(self.m_Window)

    def delete(self):
        if cmds.window(self.m_Window, exists=True):
            cmds.deleteUI(self.m_Window)


def ChangeColor(color):
    sels = cmds.ls(sl=True)
    for sel in sels:
        shapes = cmds.listRelatives(sel, children=True, shapes=True)
        for shape in shapes:
            cmds.setAttr("%s.overrideEnabled" % (shape), True)
            cmds.setAttr("%s.overrideColor" % (shape), color)
    return


myUI = ToolUI()
myUI.create()
#myUI.delete()
#myUI.show()