using Microsoft.EntityFrameworkCore;
using SpiralDev.Api.Models;

namespace SpiralDev.Api.Data;

/// <summary>
/// Llena la base de datos con el contenido inicial del libro
/// "C para Ingeniería Electrónica" de Jorge Argibay (Capítulo 2: Fundamentos de C).
/// Se ejecuta una sola vez al arrancar la app en modo desarrollo.
/// </summary>
public static class DbSeeder
{
    public static void Seed(SpiralDbContext context)
    {
        // Si ya hay cursos, no sembramos de nuevo
        if (context.Courses.Any())
        {
            return;
        }

        // ===== CARRERA: C =====
        var courseC = new Course
        {
            Name = "C",
            Description = "Bajo nivel: memoria, punteros, hardware",
            Topics = []
        };

        // ===== TEMA: Fundamentos de C (Capítulo 2) =====
        var topicFundamentos = new Topic
        {
            Order = 1,
            Title = "Fundamentos de C",
            Lessons = []
        };

        // ===== LECCIÓN 1: Estructura de un programa =====
        var lessonEstructura = new Lesson
        {
            Order = 1,
            Title = "Estructura de un programa C",
            ContentMarkdown = """
                ## Estructura de un programa C

                Todo programa C se basa en **funciones**. Siempre debe estar presente al menos una que actúa como eje del programa: la función `main()`.

                ```
                [Comandos al compilador (preprocesador)]
                [Prototipos de funciones]
                [Declaración de variables globales]

                int main()
                {
                    [Declaración de variables locales]
                    [Cuerpo del programa]
                }

                [Cuerpo de las funciones propias]
                ```

                ### Comandos al preprocesador
                Comienzan con el símbolo `#` (numeral). El más común es `#include <stdio.h>`, que incluye las funciones de entrada/salida.

                ### Variables
                Las variables son **etiquetas para áreas de la memoria RAM**. La declaración reserva la memoria necesaria.

                > **Importante**: la sangría (tabulación) no es un requerimiento del compilador — es una *costumbre* que facilita la lectura. El código se escribe y lee para seres humanos.

                ### Código de ejemplo

                ```c
                #include <stdio.h>

                int main()
                {
                    printf("Hola Mundo\n");
                    return 0;
                }
                ```
                """,
            Exercises = []
        };

        // ===== LECCIÓN 2: Tipos de datos y modificadores =====
        var lessonTipos = new Lesson
        {
            Order = 2,
            Title = "Tipos de datos y modificadores",
            ContentMarkdown = """
                ## Tipos de datos

                C utiliza **cinco palabras reservadas** para los tipos fundamentales:

                | Tipo | Longitud | Rango (aprox.) |
                |------|----------|----------------|
                | `char` | 8 bits | -128 a 127 |
                | `int` | 32 bits | -2.147M a 2.147M |
                | `float` | 32 bits | 3.4E-38 a 3.4E+38 |
                | `double` | 64 bits | 1.7E-308 a 1.7E+308 |
                | `void` | - | sin valor |

                ### Dato clave: `char` NO almacena caracteres
                Almacena **números binarios** de 8 bits. Se usa para caracteres porque su longitud coincide con el código **ASCII**.

                ### Modificadores
                Alteran el tamaño o comportamiento del tipo:

                - `long` / `short` → cambian la longitud
                - `unsigned` → sin negativos (rango 0 a 2ⁿ-1)
                - `const` → lectura solamente (acceso tipo ROM)
                - `static` → variable local con área global

                ```
                modificador tipo nombre_de_variable;
                ```

                ```c
                int main()
                {
                    int edad;               // entero signado
                    unsigned char letra;    // 0 a 255
                    const float PI = 3.14;  // no se puede modificar
                    return 0;
                }
                ```
                """,
            Exercises = []
        };

        // ===== LECCIÓN 3: Operadores =====
        var lessonOperadores = new Lesson
        {
            Order = 3,
            Title = "Operadores, precedencia y casting",
            ContentMarkdown = """
                ## Operadores aritméticos

                | Operador | Tipo | Función |
                |----------|------|---------|
                | `-` | Monario | Signo negativo |
                | `-` | Binario | Resta |
                | `+` | Binario | Suma |
                | `*` | Binario | Multiplicación |
                | `/` | Binario | División |
                | `%` | Binario | Resto de división |
                | `++` / `--` | Monario | Incremento / Decremento |

                ### Pre y post incremento

                ```c
                // Caso 1: preincremento → A=2, B = ++A → A y B quedan en 3
                // Caso 2: postincremento → A=2, B = A++ → A queda 3, B queda 2
                ```

                **Regla**: si el `++` está *antes*, incrementa y luego usa. Si está *después*, usa y luego incrementa.

                ### Precedencia
                En expresiones sin paréntesis se resuelve primero:

                ```
                1º  ++  --  (monario) -
                2º  *   /   %
                3º  +   -
                ```

                ### Casting
                Conversión momentánea de tipo: `(tipo) expresion`

                ```c
                int A = 18, B = 5;
                float F;
                F = (float) A / B;   // 3.6 (sin cast sería 3)
                ```

                > **Trampa clásica**: `18 / 5` en enteros da `3`, ¡la parte fraccionaria se pierde!
                """,
            Exercises = []
        };

        // ===== LECCIÓN 4: Entrada y salida de consola =====
        var lessonEntradaSalida = new Lesson
        {
            Order = 4,
            Title = "Entrada y salida de consola",
            ContentMarkdown = """
                ## Entrada y salida de consola

                C resuelve E/S mediante **funciones y macros**, no con instrucciones nativas. Todas viven en `stdio.h`.

                ### Salida: `printf()`

                ```c
                int printf(cadena_de_formato, lista_de_valores);
                ```

                ```c
                printf("Hola Mundo\n");            // solo texto
                printf("El valor es %d", A);       // con entero
                printf("%d / %d = %.2f", A, B, (float)A/B);
                ```

                | Formato | Tipo |
                |---------|------|
                | `%d` | entero decimal con signo |
                | `%u` | entero decimal sin signo |
                | `%c` | carácter |
                | `%f` | real |
                | `%s` | string |
                | `%p` | puntero (dirección) |

                ### Entrada: `scanf()`

                ```c
                int scanf(cadena_de_formato, lista_de_direcciones);
                ```

                > **Clave**: scanf necesita la **dirección** de la variable, no su valor. Por eso se usa `&`:

                ```c
                int A;
                scanf("%d", &A);   // &A = "la dirección de A"
                ```

                ### Ejemplo completo: superficie del triángulo

                ```c
                #include <stdio.h>
                int main()
                {
                    float BASE, ALTURA, SUPERFICIE;
                    printf("BASE = ");
                    scanf("%f", &BASE);
                    printf("ALTURA = ");
                    scanf("%f", &ALTURA);
                    SUPERFICIE = BASE * ALTURA / 2;
                    printf("SUPERFICIE = %.2f\n", SUPERFICIE);
                    return 0;
                }
                ```
                """,
            Exercises = []
        };

        // ===== EJERCICIOS (Problemas propuestos del Cap. 2) =====

        var ejerciciosLeccion4 = new List<Exercise>
        {
            // Ejercicio de código real — problema propuesto N°1
            new()
            {
                Order = 1,
                Type = ExerciseType.CodeWriting,
                Title = "Circunferencia y superficie del círculo",
                Statement = "Permitir el ingreso del radio (flotante) e imprimir en pantalla la longitud de la circunferencia y la superficie del círculo correspondiente.",
                StarterCode = """
                    #include <stdio.h>

                    int main()
                    {
                        float RADIO, LONGITUD, SUPERFICIE;
                        printf("RADIO = ");
                        scanf("%f", &RADIO);

                        // LONGITUD = 2 * PI * RADIO
                        // SUPERFICIE = PI * RADIO * RADIO

                        printf("LONGITUD = %.2f\n", LONGITUD);
                        printf("SUPERFICIE = %.2f\n", SUPERFICIE);
                        return 0;
                    }
                    """,
                ExpectedOutput = "RADIO = LONGITUD = 31.42\nSUPERFICIE = 78.54\n",
                RequiredTopicIds = [1, 2]
            },

            // Ejercicio de código real — problema propuesto N°2
            new()
            {
                Order = 2,
                Type = ExerciseType.CodeWriting,
                Title = "Promedio de 3 valores",
                Statement = "Ingresar 3 valores enteros y calcular su promedio. Cuidado: ¡la división de enteros pierde decimales!",
                StarterCode = """
                    #include <stdio.h>

                    int main()
                    {
                        int A, B, C;
                        float PROM;

                        printf("Ingrese 3 valores: ");
                        scanf("%d %d %d", &A, &B, &C);

                        // Calcular el promedio en PROM
                        // (usá casting o divide por 3.0)

                        printf("El promedio es %.2f\n", PROM);
                        return 0;
                    }
                    """,
                ExpectedOutput = "Ingrese 3 valores: El promedio es 20.00\n",
                RequiredTopicIds = [1, 2, 3]
            },

            // Ejercicio de código real — problema propuesto N°3
            new()
            {
                Order = 3,
                Type = ExerciseType.CodeWriting,
                Title = "Superficie del rombo",
                Statement = "Realizar un programa que permita el ingreso de las diagonales de un rombo y muestre el valor de su superficie. Fórmula: SUPERFICIE = D * d / 2",
                StarterCode = """
                    #include <stdio.h>

                    int main()
                    {
                        float DIAG_MAYOR, DIAG_MENOR, SUPERFICIE;

                        printf("DIAGONAL MAYOR = ");
                        scanf("%f", &DIAG_MAYOR);
                        printf("DIAGONAL MENOR = ");
                        scanf("%f", &DIAG_MENOR);

                        // SUPERFICIE = DIAG_MAYOR * DIAG_MENOR / 2

                        printf("SUPERFICIE = %.2f\n", SUPERFICIE);
                        return 0;
                    }
                    """,
                ExpectedOutput = "DIAGONAL MAYOR = DIAGONAL MENOR = SUPERFICIE = 24.00\n",
                RequiredTopicIds = [1, 2, 3]
            },

            // Ejercicio de código real — problema propuesto N°9
            new()
            {
                Order = 4,
                Type = ExerciseType.CodeWriting,
                Title = "Factura con descuento",
                Statement = "Ingresar el total de una factura (float) y el porcentaje a descontar (otro float). Mostrar el precio final. Ej: $120 con 8.8% → $109.44",
                StarterCode = """
                    #include <stdio.h>

                    int main()
                    {
                        float TOTAL, DESCUENTO, PRECIO_FINAL;

                        printf("TOTAL = ");
                        scanf("%f", &TOTAL);
                        printf("DESCUENTO %% = ");
                        scanf("%f", &DESCUENTO);

                        // PRECIO_FINAL = TOTAL - (TOTAL * DESCUENTO / 100)

                        printf("PRECIO FINAL = %.2f\n", PRECIO_FINAL);
                        return 0;
                    }
                    """,
                ExpectedOutput = "TOTAL = DESCUENTO % = PRECIO FINAL = 109.44\n",
                RequiredTopicIds = [1, 2, 3]
            },

            // Ejercicio de conceptos — Multiple choice
            new()
            {
                Order = 5,
                Type = ExerciseType.MultipleChoice,
                Title = "¿Qué imprime este código?",
                Statement = "Conceptos: operadores y precedencia.",
                Question = "¿Cuál es el valor de F en esta expresión?\n\nint A = 3, B = 2, C = 5, F;\nF = C + B * A;",
                Options = "11;21;16;17",
                CorrectOptionIndex = 0,
                StarterCode = "",
                ExpectedOutput = "",
                RequiredTopicIds = [3]
            },

            // Ejercicio de conceptos — Multiple choice
            new()
            {
                Order = 6,
                Type = ExerciseType.MultipleChoice,
                Title = "El misterio de scanf",
                Statement = "Conceptos: entrada de datos.",
                Question = "¿Por qué scanf necesita el operador &?\n\nscanf(\"%d\", &A);",
                Options = "Para enviar el valor de A a la pantalla;Porque scanf necesita la DIRECCIÓN de memoria de A para guardar el valor;Porque & es el operador de multiplicación;No es necesario, es opcional",
                CorrectOptionIndex = 1,
                StarterCode = "",
                ExpectedOutput = "",
                RequiredTopicIds = [1, 2]
            }
        };

        lessonEntradaSalida.Exercises.AddRange(ejerciciosLeccion4);

        // Ensamblamos la jerarquía completa
        topicFundamentos.Lessons.AddRange(
            [lessonEstructura, lessonTipos, lessonOperadores, lessonEntradaSalida]);
        courseC.Topics.Add(topicFundamentos);

        // ===== CARRERA: C# (vacía por ahora, se llena en el próximo sprint) =====
        var courseCs = new Course
        {
            Name = "C#",
            Description = "Orientado a objetos: POO, LINQ, .NET",
            Topics = []
        };

        context.Courses.AddRange([courseC, courseCs]);
        context.SaveChanges();

        Console.WriteLine("✅ Seed completado: carrera C con capítulo 'Fundamentos de C' (4 lecciones, 6 ejercicios)");
    }
}
