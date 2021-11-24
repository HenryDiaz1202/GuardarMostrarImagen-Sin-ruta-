Imports System.Data.Sql
Imports System.Data.SqlClient

Module Conexion
    Public conexiones As SqlConnection
    Public enunciado As SqlCommand
    Public respuesta As SqlDataReader
    Public adaptador As SqlDataAdapter


    'Procedimiento realizado para acceder a una base de datos'
    Sub abrir()
        Try
            conexiones = New SqlConnection("Data Source=HENRYDIAZ;Initial Catalog=Animes;Integrated Security=True")
            conexiones.Open()
            'MsgBox("Conexion exitosa", MsgBoxStyle.Information, "Se ha conectado correctamente")'
        Catch ex As Exception
            MsgBox("Error al realizar la conexion" & ex.Message, MsgBoxStyle.Critical, "Error de conexion")
            conexiones.Close() 'Cierra la conexion'

        End Try
    End Sub

    'Cierra y abre la conexion'
    Sub reconectar()
        conexiones.Close()
        conexiones.Open()
    End Sub

    'Guardar imagen como un String
    Function insertarImagen(ByVal ID_Anime As String, ByVal fotos As String, ByVal Nombre As String, ByVal caps As String, ByVal temps As String, ByVal pelis As String, ByVal estado As String) As String

        Dim resultado As String = ""
        reconectar()
        Try
            enunciado = New SqlCommand("insert into Animes2(ID_Anime,fotos,Nombre,caps,temps,pelis,estado) values('" & ID_Anime & "','" & fotos & "','" & Nombre & "','" & caps & "','" & temps & "','" & pelis & "','" & estado & "')", conexiones)
            enunciado.ExecuteNonQuery()
            resultado = "Se realizo correctamente la insercion"
            conexiones.Close()

        Catch ex As Exception
            resultado = "No se pudo realizar el registro de manera correcta" + ex.ToString
            conexiones.Close()

        End Try
        Return resultado
    End Function

    
    'Retorna un String apartir de un arreglo de Bytes. Concatena cada elemento en la variable salida para ser almacenada en la Base de Datos'
    Function bytesToString(ByVal arreglo As Byte()) As String
        Dim salida As String = ""
        Dim x As Integer = 0
        'MsgBox("Tamaño del arreglo: " + arreglo.Length.ToString)
        Try
            For x = 0 To arreglo.Length - 1
                salida += arreglo(x).ToString + ","
            Next
        Catch ex As Exception
            MsgBox("No lo convertio a String por: " + ex.ToString)
        End Try
       
        Return salida
    End Function

    'Consulta el String que Bytes() almacenados en la base de datos y convierte ese String en un Arreglo de bytes() '
    Function consultaByte(ByVal identificacion As String) As Byte()
        reconectar()
        Dim resultado As String = ""
        Dim x As Integer = 0
        Dim arreglo As Byte() = Nothing
        Dim arregloTexto()


        Try
            enunciado = New SqlCommand("select fotos from Animes2 where ID_Anime ='" & identificacion & "'", conexiones)
            respuesta = enunciado.ExecuteReader()

            While respuesta.Read
                resultado = respuesta.Item("fotos")
            End While

            'Llena un arreglo de Texto con los datos de la consulta separados por coma"'
            arregloTexto = resultado.Split(",")

            'Redimenciona el tamaño del arreglo de bytes'
            ReDim arreglo(arregloTexto.Length - 1)

            'Recorre el arreglo para llenar el arreglo de Bytes con el arreglo de la consulta'

            For x = 0 To arregloTexto.Length - 1
                If arregloTexto(x).Equals("") = False Then
                    arreglo(x) = arregloTexto(x)
                End If
            Next
        
            respuesta.Close()
        Catch ex As Exception

        End Try

        Return arreglo
    End Function

    'Carga en un comboBox el nombre de todas las imagenes almacenadas
    Function listaImagenes(ByVal cb As ComboBox) As String
        reconectar()
        Dim resultado As String = ""
        Try
            enunciado = New SqlCommand("select distinct ID_Anime from Animes2", conexiones)
            respuesta = enunciado.ExecuteReader()

            While respuesta.Read
                cb.Items.Add(respuesta.Item("ID_Anime"))

            End While
        Catch ex As Exception

        End Try
        Return resultado

    End Function


End Module
