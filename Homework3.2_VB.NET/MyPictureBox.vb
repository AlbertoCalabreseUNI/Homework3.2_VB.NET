Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing
Imports System

Public Class MyPictureBox
    Inherits PictureBox

    Private point As Point
    Private MouseIsInLeftEdge As Boolean
    Private MouseIsInRightEdge As Boolean
    Private MouseIsInTopEdge As Boolean
    Private MouseIsInBottomEdge As Boolean
    Private currentHeight As Integer
    Private currentWidth As Integer

    Public Sub New(ByVal container As IContainer)
        container.Add(Me)
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        point = e.Location
        MouseIsInLeftEdge = Math.Abs(point.X) <= 10
        MouseIsInRightEdge = Math.Abs(point.X - Width) <= 10
        MouseIsInTopEdge = Math.Abs(point.Y) <= 10
        MouseIsInBottomEdge = Math.Abs(point.Y - Height) <= 10
        currentHeight = Height
        currentWidth = Width
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If MouseIsInLeftEdge Then
                If MouseIsInTopEdge Then
                    Width -= e.X - point.X
                    Left += e.X - point.X
                    Height -= e.Y - point.Y
                    Top += e.Y - point.Y
                ElseIf MouseIsInBottomEdge Then
                    Width -= e.X - point.X
                    Left += e.X - point.X
                    Height = e.Y - point.Y + currentHeight
                Else
                    Width -= e.X - point.X
                    Left += e.X - point.X
                End If
            ElseIf MouseIsInRightEdge Then

                If MouseIsInTopEdge Then
                    Width = e.X - point.X + currentWidth
                    Height -= e.Y - point.Y
                    Top += e.Y - point.Y
                ElseIf MouseIsInBottomEdge Then
                    Width = e.X - point.X + currentWidth
                    Height = e.Y - point.Y + currentHeight
                Else
                    Width = e.X - point.X + currentWidth
                End If
            ElseIf MouseIsInTopEdge Then
                Height -= e.Y - point.Y
                Top += e.Y - point.Y
            ElseIf MouseIsInBottomEdge Then
                Height = e.Y - point.Y + currentHeight
            Else
                Left += e.X - point.X
                Top += e.Y - point.Y
            End If
        End If

        MyBase.OnMouseMove(e)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
    End Sub
End Class
