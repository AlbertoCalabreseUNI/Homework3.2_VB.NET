Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

Namespace Homework3._2_CSharp
    Public Partial Class Form1
        Inherits Form

        Public book As IEnumerable(Of String)
        Public words As Dictionary(Of String, Integer)
        Public blacklist As IEnumerable(Of String)
        Public rectangles As List(Of MyRectangle)
        Private timer As Timer
        ' 
        '  My Frequencies: Word appearing once or twice, word appearing three or four times, words appearing 5+ times.
        ' 
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
                        ''' Cannot convert AssignmentExpressionSyntax, System.NullReferenceException: Object reference not set to an instance of an object.
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
''' 
''' 
'''             myPictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.myPictureBox1_Paint)
''' 
            Controls.Add(Me.myPictureBox1)
            AddHandler timer.Tick, New EventHandler(AddressOf timer1_Tick)
            timer.Interval = 50
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)
            'With this we filter out all the words that are less than 2 chars long and we discard numbers. Additionaly you
            ' can use the populateBlacklist() function to populate the blacklist in order to discard specific words.
            Dim n As Integer = Nothing

            For Each line In book
                Dim ws = line.Split()

                For Each word In ws
                    If word.Length < 2 OrElse Integer.TryParse(word, n) OrElse blacklist.Contains(word.ToLower()) Then Continue For

                    If Not words.ContainsKey(word) Then
                        words.Add(word, 1)
                    Else
                        words(word) += 1
                    End If
                Next
            Next
            'LINQ to sort by value
            'We discard any word that has only happeared once.
            Dim sortedDict = From entry In words Where entry.Value > 1 Order By entry.Value Select entry

            For Each word In sortedDict
                If word.Value = 2 Then distribution2 += 1
                If word.Value = 3 OrElse word.Value = 4 Then distribution3_4 += 1
                If word.Value >= 5 Then distribution5 += 1
            Next

            Dim color As SolidBrush = New SolidBrush(Drawing.Color.FromArgb(255, 0, 0))
            rectangles.Add(New MyRectangle(20, distribution2 / 3, label1.Location.X, myPictureBox1.Height, color, myPictureBox1))
            rectangles.Add(New MyRectangle(20, distribution3_4 / 3, label2.Location.X, myPictureBox1.Height, color, myPictureBox1))
            rectangles.Add(New MyRectangle(20, distribution5 / 3, label3.Location.X, myPictureBox1.Height, color, myPictureBox1))
            timer.Start()
        End Sub

        Private Sub timer1_Tick(ByVal sender As Object, ByVal e As EventArgs)
            Me.myPictureBox1.Refresh()
        End Sub

        Private Sub myPictureBox1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
            Dim g = e.Graphics

            For Each rect As MyRectangle In rectangles
                rect.Draw(g)
                rect.Update()
            Next
        End Sub
    End Class
End Namespace
