Imports System
Imports EngineeringMathPack
Module Program
    Sub Main(args As String())
        Dim arr1(,) As Object = {{1, 2, 3, 4},
                                    {1, 2, 3, 4},
                                    {1, 2, 3, 4}}
        Dim m1 As New Matrix(arr1)
        Console.WriteLine(m1.ToString)
        Console.WriteLine()
        Console.WriteLine(m1.Transpose.ToString)
        Console.ReadLine()

        Dim arr2() As Object = {1, 2, 3, 4, 5, 6}
        Dim m2 As New Matrix(arr2)
        Dim m3 As New Matrix(arr2, Matrix.Direction.ColumnDirection)
        Console.WriteLine(m2.ToString)
        Console.WriteLine(m3.ToString)
        Console.ReadLine()

        Dim m4 As Matrix
        m4 = m3 * m2
        Console.WriteLine(m4.ToString)
        Console.ReadLine()

        Dim m5 As SquareMatrix = SquareMatrix.GetIdentityMatrix(5)
        Dim m6 As Matrix = m5 * 10
        Console.WriteLine(m5.ToString)
        Console.WriteLine(m6.ToString)
        Console.ReadLine()

        Dim arr3(,) As Object = {{1, 2, 3, 4, 5},
                                 {1, 2, 3, 4, 5},
                                 {1, 2, 3, 4, 5},
                                 {1, 2, 3, 4, 5},
                                 {1, 2, 3, 4, 5}}
        Dim m7 As New Matrix(arr3)
        Dim m8 As SquareMatrix = Nothing
        Dim rtn_bool As Boolean = SquareMatrix.TryParse(m7, m8)
        Dim m9 As SquareMatrix
        m9 = m8 ^ 2
        Console.WriteLine(m9.ToString)
        Console.ReadLine()

        Dim arr4(,) As Object = {{1, 2, -4},
                                {-2, 2, 1},
                                {-3, 4, -2}}
        Dim m10 As New SquareMatrix(arr4)
        Console.WriteLine(m10.ToString)
        Console.WriteLine("det= " & m10.GetDeterminant)
        Console.ReadLine()

        Console.ReadLine()
    End Sub
End Module
