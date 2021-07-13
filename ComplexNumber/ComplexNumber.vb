Public Class ComplexNumber
    ''' <summary>
    ''' 坐标形式
    ''' </summary>
    Public Enum ComplexPlane As Byte
        ''' <summary>
        ''' 笛卡尔坐标形式
        ''' </summary>
        Cartesian = 0
        ''' <summary>
        ''' 极坐标形式
        ''' </summary>
        Polar = 1
    End Enum
    Private re_double As Double
    Private im_double As Double
    ''' <summary>
    ''' 复数的实部
    ''' </summary>
    ''' <returns></returns>
    Public Property Re As Double
        Get
            Return re_double
        End Get
        Set(value As Double)
            re_double = value
        End Set
    End Property
    ''' <summary>
    ''' 复数的虚部
    ''' </summary>
    ''' <returns></returns>
    Public Property Im As Double
        Get
            Return im_double
        End Get
        Set(value As Double)
            im_double = value
        End Set
    End Property
    ''' <summary>
    ''' 复数的模
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Modulus As Double
        Get
            Return Math.Sqrt(re_double ^ 2 + im_double ^ 2)
        End Get
    End Property
    ''' <summary>
    ''' 复数的相角
    ''' </summary>
    ''' <returns></returns>
    Public Property Phase
        Get
            If re_double = 0 And im_double = 0 Then
                Return 0
            Else
                Return Math.Atan2(im_double, re_double)
            End If
        End Get
        Set(value)
            Dim r As Double = Modulus
            If r > 0 Then
                re_double = r * Math.Cos(value)
                im_double = r * Math.Sin(value)
            End If
        End Set
    End Property
    ''' <summary>
    ''' 共轭复数
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Conjugate As ComplexNumber
        Get
            Return New ComplexNumber(re_double, -im_double)
        End Get
    End Property
    ''' <summary>
    ''' 默认构造函数
    ''' </summary>
    Public Sub New()
        re_double = 0
        im_double = 0
    End Sub
    ''' <summary>
    ''' 重载构造函数
    ''' </summary>
    ''' <param name="real"></param>
    ''' <param name="imaginary"></param>
    Public Sub New(ByVal real As Double, ByVal imaginary As Double)
        re_double = real
        im_double = imaginary
    End Sub
    ''' <summary>
    ''' 重载构造函数
    ''' </summary>
    ''' <param name="a">参数a：当使用笛卡尔坐标时为复数的实部，当使用极坐标时为复数的模</param>
    ''' <param name="b">参数b：当使用笛卡尔坐标时为复数的虚部，当使用极坐标时为复数的相角(弧度)</param>
    ''' <param name="plane">坐标形式</param>
    Public Sub New(ByVal a As Double, ByVal b As Double, ByVal plane As ComplexPlane)
        If plane = ComplexPlane.Cartesian Then
            re_double = a
            im_double = b
        Else
            If a = 0 Then
                re_double = 0
                im_double = 0
            Else
                a = Math.Abs(a)
                re_double = a * Math.Cos(b)
                im_double = a * Math.Sin(b)
            End If
        End If
    End Sub
    ''' <summary>
    ''' 重载ToString函数
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Function ToString() As String
        If im_double = 0 Then
            Return re_double.ToString
        ElseIf im_double > 0 Then
            Return re_double.ToString & "+" & im_double.ToString
        Else
            Return re_double.ToString & "-" & Math.Abs(im_double).ToString
        End If
    End Function
    ''' <summary>
    ''' 重载=运算符
    ''' </summary>
    ''' <param name="complex1"></param>
    ''' <param name="complex2"></param>
    ''' <returns></returns>
    Overloads Shared Operator =(ByVal complex1 As ComplexNumber, ByVal complex2 As ComplexNumber) As Boolean
        If complex1.Re = complex2.Re And complex1.Im = complex2.Im Then
            Return True
        Else
            Return False
        End If
    End Operator
    ''' <summary>
    ''' 重载=运算符
    ''' </summary>
    ''' <param name="complex"></param>
    ''' <param name="real"></param>
    ''' <returns></returns>
    Overloads Shared Operator =(ByVal complex As ComplexNumber, ByVal real As Double) As Boolean
        If complex.Im <> 0 Then
            Return False
        ElseIf complex.Re = real Then
            Return True
        Else
            Return False
        End If
    End Operator
    ''' <summary>
    ''' 重载=运算符
    ''' </summary>
    ''' <param name="real"></param>
    ''' <param name="complex"></param>
    ''' <returns></returns>
    Overloads Shared Operator =(ByVal real As Double, ByVal complex As ComplexNumber) As Boolean
        Return complex = real
    End Operator
    ''' <summary>
    ''' 重载 不等于 运算符
    ''' </summary>
    ''' <param name="complex1"></param>
    ''' <param name="complex2"></param>
    ''' <returns></returns>
    Overloads Shared Operator <>(ByVal complex1 As ComplexNumber, ByVal complex2 As ComplexNumber) As Boolean
        If complex1 = complex2 Then
            Return False
        Else
            Return True
        End If
    End Operator
    ''' <summary>
    ''' 重载 不等于 运算符
    ''' </summary>
    ''' <param name="complex"></param>
    ''' <param name="real"></param>
    ''' <returns></returns>
    Overloads Shared Operator <>(ByVal complex As ComplexNumber, ByVal real As Double) As Boolean
        If complex = real Then
            Return False
        Else
            Return True
        End If
    End Operator
    Overloads Shared Operator <>(ByVal real As Double, complex As ComplexNumber) As Boolean
        Return complex <> real
    End Operator
    ''' <summary>
    ''' 重载+运算
    ''' </summary>
    ''' <param name="complex1"></param>
    ''' <param name="complex2"></param>
    ''' <returns></returns>
    Overloads Shared Operator +(ByVal complex1 As ComplexNumber, ByVal complex2 As ComplexNumber) As ComplexNumber
        Return New ComplexNumber(complex1.Re + complex2.Re, complex1.Im + complex2.Im)
    End Operator
    ''' <summary>
    ''' 重载-运算
    ''' </summary>
    ''' <param name="complex1"></param>
    ''' <param name="complex2"></param>
    ''' <returns></returns>
    Overloads Shared Operator -(ByVal complex1 As ComplexNumber, ByVal complex2 As ComplexNumber) As ComplexNumber
        Return New ComplexNumber(complex1.Re - complex2.Re, complex1.Im - complex2.Im)
    End Operator
    ''' <summary>
    ''' 重载*运算
    ''' </summary>
    ''' <param name="complex1"></param>
    ''' <param name="complex2"></param>
    ''' <returns></returns>
    Overloads Shared Operator *(ByVal complex1 As ComplexNumber, ByVal complex2 As ComplexNumber) As ComplexNumber
        Return New ComplexNumber(complex1.Re * complex2.Re - complex1.Im * complex2.Im,
                                 complex1.Im * complex2.Re + complex1.Re * complex2.Im)
    End Operator
    ''' <summary>
    ''' 重载/运算符
    ''' </summary>
    ''' <param name="complex1"></param>
    ''' <param name="complex2"></param>
    ''' <returns></returns>
    Overloads Shared Operator /(ByVal complex1 As ComplexNumber, ByVal complex2 As ComplexNumber) As ComplexNumber
        If complex2 = 0 Then
            Return Nothing
        Else
            Return New ComplexNumber((complex1.Re * complex2.Re + complex1.Im * complex2.Im) / (complex2.Re ^ 2 + complex2.Im ^ 2), (complex1.Im * complex2.Re - complex1.Re * complex2.Im) / (complex2.Re ^ 2 + complex2.Im ^ 2))
        End If
    End Operator
    ''' <summary>
    ''' 重载 ^ 运算符，计算整数幂运算
    ''' </summary>
    ''' <param name="base">底数</param>
    ''' <param name="power">指数</param>
    ''' <returns></returns>
    Overloads Shared Operator ^(ByVal base As ComplexNumber, ByVal power As UInteger) As ComplexNumber
        Return New ComplexNumber(base.Modulus ^ power, base.Phase * power, ComplexPlane.Polar)
    End Operator
    ''' <summary>
    ''' 计算复数的n次方根，仅取相角最小值
    ''' </summary>
    ''' <param name="number">要计算的复数</param>
    ''' <param name="n">n次方</param>
    ''' <returns></returns>
    Shared Function Root(ByVal number As ComplexNumber, ByVal n As UInteger) As ComplexNumber
        '起始相角
        Dim start_phase As Double = (number.Phase + Math.PI) Mod （2 * Math.PI）
        '当前最小相角
        Dim min_phase As Double = start_phase
        For i As UInteger = 0 To n - 1
            Dim new_phase As Double = ((start_phase + 2 * i * Math.PI) / n) Mod (2 * Math.PI)
            If new_phase < min_phase Then min_phase = new_phase
        Next
        Return New ComplexNumber(number.Modulus ^ (1 / n), min_phase, ComplexPlane.Polar)
    End Function
End Class
