Public Class ComplexNumber
    Private re_double As Double '实部变量
    Private im_double As Double '虚部变量
    ''' <summary>
    ''' 坐标形式
    ''' </summary>
    Public Enum ComplexPlane As Byte
        ''' <summary>
        ''' 笛卡尔坐标形式
        ''' </summary>
        Cartesian = 0
        ''' <summary>
        ''' 极坐标形式，相角为弧度
        ''' </summary>
        PolarRadian = 1
        ''' <summary>
        ''' 极坐标形式，相角为度数
        ''' </summary>
        PolarDegree = 2
    End Enum
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
    ''' 复数的相角(弧度,-pi至+pi)
    ''' </summary>
    ''' <returns></returns>
    Public Property PhaseRadian As Double
        Get
            If re_double = 0 And im_double = 0 Then
                Return 0
            Else
                'atan2的返回值在-pi至+pi之间
                Return Math.Atan2(im_double, re_double)
            End If
        End Get
        Set(value As Double)
            Dim r As Double = Modulus
            If r > 0 Then
                re_double = r * Math.Cos(value)
                im_double = r * Math.Sin(value)
            End If
        End Set
    End Property
    ''' <summary>
    ''' 复数的相角(度数,-180至+180)
    ''' </summary>
    ''' <returns></returns>
    Public Property PhaseDegree As Double
        Get
            Return PhaseRadian * 180 / Math.PI
        End Get
        Set(value As Double)
            PhaseRadian = value / 180 * Math.PI
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
    ''' <param name="b">参数b：当使用笛卡尔坐标时为复数的虚部，当使用极坐标时为复数的相角</param>
    ''' <param name="plane">坐标形式</param>
    Public Sub New(ByVal a As Double, ByVal b As Double, ByVal plane As ComplexPlane)
        Select Case plane
            Case ComplexPlane.Cartesian
                re_double = a
                im_double = b
            Case ComplexPlane.PolarRadian
                a = Math.Abs(a)
                re_double = a * Math.Cos(b)
                im_double = a * Math.Sin(b)
            Case ComplexPlane.PolarDegree
                a = Math.Abs(a)
                re_double = a * Math.Cos(b / 180 * Math.PI)
                im_double = a * Math.Sin(b / 180 * Math.PI)
        End Select
    End Sub
    ''' <summary>
    ''' 重载ToString函数
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Function ToString() As String
        Return ToString(ComplexPlane.Cartesian)
    End Function
    ''' <summary>
    ''' 重载ToString函数
    ''' </summary>
    ''' <param name="plane"></param>
    ''' <returns></returns>
    Public Overloads Function ToString(ByVal plane As ComplexPlane) As String
        Return ToString("0.####", plane)
    End Function
    ''' <summary>
    ''' 重载ToString函数
    ''' </summary>
    ''' <param name="format"></param>
    ''' <param name="plane"></param>
    ''' <returns></returns>
    Public Overloads Function ToString(ByVal format As String, ByVal plane As ComplexPlane) As String
        If Me = 0 Then
            Return 0
        Else
            Select Case plane
                Case ComplexPlane.Cartesian
                    If im_double = 0 Then
                        Return re_double.ToString
                    ElseIf im_double > 0 Then
                        Return re_double.ToString(format) & "+" & im_double.ToString(format)
                    Else
                        Return re_double.ToString(format) & "-" & Math.Abs(im_double).ToString(format)
                    End If
                Case ComplexPlane.PolarRadian
                    Return Modulus.ToString(format) & "∠" & (PhaseRadian / Math.PI).ToString(format) & "Pi"
                Case ComplexPlane.PolarDegree
                    Return Modulus.ToString(format) & "∠" & PhaseDegree.ToString(format) & "°"
                Case Else
                    Return "Unknown ComplexPlane"
            End Select
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
        Return complex = New ComplexNumber(real, 0)
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
    ''' 重载+运算
    ''' </summary>
    ''' <param name="complex"></param>
    ''' <param name="real"></param>
    ''' <returns></returns>
    Overloads Shared Operator +(ByVal complex As ComplexNumber, ByVal real As Double) As ComplexNumber
        Return complex + New ComplexNumber(real, 0)
    End Operator
    ''' <summary>
    ''' 重载+运算
    ''' </summary>
    ''' <param name="real"></param>
    ''' <param name="complex"></param>
    ''' <returns></returns>
    Overloads Shared Operator +(ByVal real As Double, ByVal complex As ComplexNumber) As ComplexNumber
        Return complex + real
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
    ''' 重载-运算
    ''' </summary>
    ''' <param name="complex"></param>
    ''' <param name="real"></param>
    ''' <returns></returns>
    Overloads Shared Operator -(ByVal complex As ComplexNumber, ByVal real As Double) As ComplexNumber
        Return complex - New ComplexNumber(real, 0)
    End Operator
    ''' <summary>
    ''' 重载-运算
    ''' </summary>
    ''' <param name="real"></param>
    ''' <param name="complex"></param>
    ''' <returns></returns>
    Overloads Shared Operator -(ByVal real As Double, ByVal complex As ComplexNumber) As ComplexNumber
        Return New ComplexNumber(real, 0) - complex
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
    ''' 重载*运算
    ''' </summary>
    ''' <param name="complex"></param>
    ''' <param name="real"></param>
    ''' <returns></returns>
    Overloads Shared Operator *(ByVal complex As ComplexNumber, ByVal real As Double) As ComplexNumber
        Return complex * New ComplexNumber(real, 0)
    End Operator
    ''' <summary>
    ''' 重载*运算
    ''' </summary>
    ''' <param name="real"></param>
    ''' <param name="complex"></param>
    ''' <returns></returns>
    Overloads Shared Operator *(ByVal real As Double, ByVal complex As ComplexNumber) As ComplexNumber
        Return complex * real
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
    ''' 重载/运算符
    ''' </summary>
    ''' <param name="complex"></param>
    ''' <param name="real"></param>
    ''' <returns></returns>
    Overloads Shared Operator /(ByVal complex As ComplexNumber, ByVal real As Double) As ComplexNumber
        Return complex / New ComplexNumber(real, 0)
    End Operator
    ''' <summary>
    ''' 重载/运算符
    ''' </summary>
    ''' <param name="real"></param>
    ''' <param name="complex"></param>
    ''' <returns></returns>
    Overloads Shared Operator /(ByVal real As Double, ByVal complex As ComplexNumber) As ComplexNumber
        Return New ComplexNumber(real, 0) / complex
    End Operator
    ''' <summary>
    ''' 重载 ^ 运算符，计算整数幂运算
    ''' </summary>
    ''' <param name="base">底数</param>
    ''' <param name="power">指数</param>
    ''' <returns></returns>
    Overloads Shared Operator ^(ByVal base As ComplexNumber, ByVal power As UInteger) As ComplexNumber
        Return New ComplexNumber(base.Modulus ^ power, base.PhaseRadian * power, ComplexPlane.PolarRadian)
    End Operator
    ''' <summary>
    ''' 计算复数的n次方根，仅取相角最小值
    ''' </summary>
    ''' <param name="number">要计算的复数</param>
    ''' <param name="n">n次方</param>
    ''' <returns></returns>
    Shared Function Root(ByVal number As ComplexNumber, ByVal n As UInteger) As ComplexNumber
        '起始相角
        Dim start_phase As Double = 2 * Math.PI
        '当前最小相角
        Dim min_phase As Double = start_phase
        For i As UInteger = 0 To n - 1
            Dim new_phase As Double = ((start_phase + 2 * i * Math.PI) / n) Mod (2 * Math.PI)
            If new_phase < min_phase Then min_phase = new_phase
        Next
        Return New ComplexNumber(number.Modulus ^ (1 / n), min_phase, ComplexPlane.PolarRadian)
    End Function
End Class