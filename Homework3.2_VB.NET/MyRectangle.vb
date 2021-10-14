Imports System.Drawing

Public Class MyRectangle
    Public Property posX As Integer
    Public Property posY As Integer
    Public Property width As Integer
    Public Property height As Integer
    Public Property finalHeight As Integer
    Public Property rectColor As Brush
    Private borderColor As Pen = Pens.Black
    Private pictureBox As MyPictureBox

    Public Sub New(ByVal w As Integer, ByVal h As Integer, ByVal x As Integer, ByVal y As Integer, ByVal color As Brush, ByVal pb As MyPictureBox)
        width = w
        height = 0
        finalHeight = h
        posX = x
        posY = pb.Height
        rectColor = color
        pictureBox = pb
    End Sub

    Public Sub Draw(ByVal g As Graphics)
        g.FillRectangle(rectColor, posX, posY, width, height)
        g.DrawRectangle(borderColor, posX, posY, width, height)
    End Sub

    Public Sub Update()
        If height >= finalHeight Then Return
        height += 5
        posY -= 5
    End Sub
End Class
