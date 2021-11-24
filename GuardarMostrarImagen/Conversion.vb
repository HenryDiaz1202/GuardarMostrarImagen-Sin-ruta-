Imports System.IO

Module Conversion


    'Funcion que convierte de Image a Byte()
    Public Function ImagenToBytes(ByVal fotos As Image) As Byte()
        'si hay imagen
        Dim arreglo As Byte() = Nothing
        Try
            If Not fotos Is Nothing Then
                'variable de datos binarios en stream(flujo)
                Dim Bin As New MemoryStream
                'convertir a bytes
                fotos.Save(Bin, Imaging.ImageFormat.Jpeg)
                'retorna binario
                arreglo = Bin.GetBuffer
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MsgBox("No convirtio a bytes por: " + ex.ToString)
        End Try
        Return arreglo
    End Function

    'Funcion que convierte de Byte() a Image
    Public Function BytesToImagen(ByVal foto As Byte()) As Image
        Try
            'si hay imagen
            If Not foto Is Nothing Then
                'caturar array con memorystream hacia Bin
                Dim Bin As New MemoryStream(foto)
                'con el método FroStream de Image obtenemos imagen
                Dim Resultado As Image = Image.FromStream(Bin)
                'y la retornamos
                Return Resultado
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

End Module
