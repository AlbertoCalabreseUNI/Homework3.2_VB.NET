Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Partial Public Class Form1
    Inherits Form

    Public book As IEnumerable(Of String)
    Public words As Dictionary(Of String, Integer)
    Public blacklist As IEnumerable(Of String)
    Public rectangles As List(Of MyRectangle)
    Private timer As Timer
    Public distribution2 As Integer = 0
    Public distribution3_4 As Integer = 0
    Public distribution5 As Integer = 0

    Public Sub New()
        InitializeComponent()
        book = File.ReadLines("Files\book.txt")
        blacklist = File.ReadLines("Files\stopwords.txt").ToList()
        words = New Dictionary(Of String, Integer)()
        timer = New Timer()
        rectangles = New List(Of MyRectangle)()


        timer.Enabled = True
        timer.Interval = 20
        timer.Stop()

        AddHandler timer.Tick, AddressOf timer1_Tick
        AddHandler MyPictureBox1.Paint, AddressOf myPictureBox1_Paint
    End Sub

    Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim n As Integer = Nothing

        For Each line As String In book
            Dim ws = line.Split()

            For Each word As String In ws
                If word.Length < 2 OrElse Integer.TryParse(word, n) OrElse blacklist.Contains(word.ToLower()) Then Continue For

                If Not words.ContainsKey(word) Then
                    words.Add(word, 1)
                Else
                    words(word) += 1
                End If
            Next
        Next

        Dim sortedDict = From entry In words Where entry.Value > 1 Order By entry.Value Select entry

        For Each word As KeyValuePair(Of String, Integer) In sortedDict
            If word.Value = 2 Then distribution2 += 1
            If word.Value = 3 OrElse word.Value = 4 Then distribution3_4 += 1
            If word.Value >= 5 Then distribution5 += 1
        Next

        Dim rectColor As SolidBrush = New SolidBrush(Color.FromArgb(255, 0, 0))
        rectangles.Add(New MyRectangle(20, distribution2 / 3, Label1.Location.X, MyPictureBox1.Height, rectColor, MyPictureBox1))
        rectangles.Add(New MyRectangle(20, distribution3_4 / 3, Label2.Location.X, MyPictureBox1.Height, rectColor, MyPictureBox1))
        rectangles.Add(New MyRectangle(20, distribution5 / 3, Label3.Location.X, MyPictureBox1.Height, rectColor, MyPictureBox1))
        timer.Start()
    End Sub

    Private Sub timer1_Tick(ByVal sender As Object, ByVal e As EventArgs)
        Me.MyPictureBox1.Refresh()
    End Sub

    Private Sub myPictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim g As Graphics = e.Graphics

        For Each rect As MyRectangle In rectangles
            rect.Draw(g)
            rect.Update()
        Next
    End Sub
End Class
