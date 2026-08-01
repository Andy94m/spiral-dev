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
        // Seed original: corre solo si falta el capítulo 2 (Fundamentos de C).
        // Así, el seed incremental de abajo agrega lo nuevo sin duplicar lo ya sembrado.
        if (!context.Topics.Any(t => t.Title == "Fundamentos de C"))
        {

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

        // ===== SEED INCREMENTAL: Capítulo 3 "Control de flujo" (idempotente) =====
        // Corre siempre; si el capítulo ya existe, no hace nada.
        if (!context.Topics.Any(t => t.Title == "Control de flujo"))
        {
            var courseC = context.Courses.Single(c => c.Name == "C");

            var topicControlFlujo = new Topic
            {
                Order = 2,
                Title = "Control de flujo",
                Lessons = []
            };

            // ===== LECCIÓN 1: if-else =====
            var lessonIfElse = new Lesson
            {
                Order = 1,
                Title = "Toma de decisiones: if-else",
                ContentMarkdown = """
                    ## Toma de decisiones

                    El procesador ejecuta las instrucciones en **secuencia natural**. Cuando un programa debe elegir entre dos caminos, rompe esa secuencia con un **salto condicional**: el salto se produce solo si se cumple una condición.

                    La sentencia `if-else` implementa la toma de decisión:

                    ```
                    if (condición)
                        sentencia A;
                    [else
                        sentencia B;]
                    ```

                    Los corchetes indican que la parte `else` es **opcional**.

                    ### Sentencias simples y compuestas
                    - **Simple**: una sola sentencia.
                    - **Compuesta**: varias sentencias encerradas entre llaves `{ }`.

                    ```c
                    if (condición)
                    {
                        sentencia A;
                        sentencia B;
                    }
                    else
                    {
                        sentencia C;
                    }
                    ```

                    > **Importante**: la tabulación muestra qué sentencias están dentro de cada bloque. Y ojo con las llaves: si un bloque tiene más de una sentencia y olvidás las llaves, las sentencias extra quedarán **fuera del bloque** (bug clásico).

                    ### Ejemplo

                    ```c
                    #include <stdio.h>

                    int main()
                    {
                        int edad = 18;

                        if (edad >= 18)
                            printf("Mayor de edad\n");
                        else
                            printf("Menor de edad\n");

                        return 0;
                    }
                    ```
                    """,
                Exercises = []
            };

            // ===== LECCIÓN 2: Condiciones y operadores =====
            var lessonCondiciones = new Lesson
            {
                Order = 2,
                Title = "Condiciones y operadores",
                ContentMarkdown = """
                    ## Condiciones y operadores

                    Una condición es una **expresión que da como resultado verdadero o falso**.

                    ### Operadores relacionales
                    Comparan dos valores (son binarios):

                    | Operador | Significado |
                    |----------|-------------|
                    | `==` | Igual que |
                    | `!=` | Distinto de |
                    | `>` | Mayor que |
                    | `<` | Menor que |
                    | `>=` | Mayor o igual |
                    | `<=` | Menor o igual |

                    El resultado de una operación relacional es un valor **booleano**. En C, `0` es FALSO y `1` es VERDADERO:

                    ```c
                    int F;
                    F = 4 > 2;   /* F toma el valor 1 (verdadero) */
                    F = 4 == 2;  /* F toma el valor 0 (falso) */
                    ```

                    > **Dato clave**: C considera que **todo valor numérico distinto de cero es VERDADERO** y que cero es FALSO. Se puede evaluar cualquier expresión numérica como condición.

                    ### Condiciones compuestas — operadores lógicos
                    Cuando una condición tiene más de una parte se usan los operadores lógicos:

                    | Operador | Tipo | Función |
                    |----------|------|---------|
                    | `!` | monario | Inversor lógico (NOT) |
                    | `&&` | binario | AND (y) |
                    | `\|\|` | binario | OR (o) |

                    Tabla de verdad del AND (`A && B`):

                    | A | B | Resultado |
                    |---|---|-----------|
                    | F | F | F |
                    | F | V | F |
                    | V | F | F |
                    | V | V | V |

                    Ejemplo:

                    ```c
                    if (edad >= 18 && tiene_dni)
                        printf("Puede votar\n");
                    ```
                    """,
                Exercises = []
            };

            // ===== LECCIÓN 3: Decisiones anidadas y escalonador =====
            var lessonEscalonador = new Lesson
            {
                Order = 3,
                Title = "Decisiones anidadas y escalonador",
                ContentMarkdown = """
                    ## Decisiones anidadas y escalonador

                    Los bloques de una toma de decisión pueden contener otras tomas de decisión: son **decisiones anidadas**. Hay que ser cuidadoso con las aperturas y cierres de llaves `{ }`.

                    ### El escalonador (else if)
                    Cuando cada `else` contiene otro `if`, se forma un **escalonador**: cada nuevo nivel de decisión se ubica en la salida por "no" del anterior.

                    ```c
                    if (condición 1)
                        sentencia 1;
                    else if (condición 2)
                        sentencia 2;
                    else if (condición 3)
                        sentencia 3;
                    else
                        sentencia 4;
                    ```

                    ### Ejemplo: clasificar socios por edad
                    Infantil (< 14), Cadete (14-20), Activo (21-59), Senior (60 o más):

                    ```c
                    #include <stdio.h>

                    int main()
                    {
                        int EDAD;

                        printf("EDAD DEL SOCIO = ");
                        scanf("%d", &EDAD);

                        if (EDAD > 59)
                            printf("SOCIO SENIOR\n");
                        else if (EDAD > 20)
                            printf("SOCIO ACTIVO\n");
                        else if (EDAD > 13)
                            printf("SOCIO CADETE\n");
                        else
                            printf("SOCIO INFANTIL\n");

                        return 0;
                    }
                    ```

                    > **Nota**: en el ejemplo se omiten las llaves porque cada bloque tiene una sola sentencia. Conviene ponerlas siempre: si después agregás una sentencia a un bloque y olvidás las llaves, quedará fuera del bloque.
                    """,
                Exercises = []
            };

            // ===== LECCIÓN 4: Selector switch-case =====
            var lessonSwitch = new Lesson
            {
                Order = 4,
                Title = "Selector switch-case",
                ContentMarkdown = """
                    ## Selector switch-case

                    El `switch-case` es un caso particular del escalonador: compara el valor de una variable contra una serie de **constantes** y desvía el flujo cuando hay coincidencia.

                    ### Restricciones
                    - Las condiciones se resuelven **solamente por igualdad**
                    - La variable a comparar debe ser de tipo **enumerable** (`int`, `char`, etc.)
                    - Los valores comparados deben ser **constantes** del programa

                    Estas pautas limitan su uso, prácticamente, a la implementación de **menús**.

                    ```c
                    switch (variable)
                    {
                        case constante1:
                            sentencia 1;
                            break;
                        case constante2:
                            sentencia 2;
                            break;
                        default:
                            sentencia por defecto;
                            break;
                    }
                    ```

                    ### La sentencia `break`
                    Provoca un salto al final del bloque. **Sin `break`, el flujo continúa** ejecutando los `case` siguientes (trampa clásica).

                    ### Ejemplo: menú de 3 opciones

                    ```c
                    #include <stdio.h>

                    int main()
                    {
                        int SEL;

                        printf("1. OPCION 1\n");
                        printf("2. OPCION 2\n");
                        printf("3. OPCION 3\n");
                        printf("Ingrese su opción: ");
                        scanf("%d", &SEL);

                        switch (SEL)
                        {
                            case 1:
                                printf("Ud. seleccionó OPCION 1\n");
                                break;
                            case 2:
                                printf("Ud. seleccionó OPCION 2\n");
                                break;
                            case 3:
                                printf("Ud. seleccionó OPCION 3\n");
                                break;
                            default:
                                printf("Ud. seleccionó otra cosa\n");
                                break;
                        }

                        return 0;
                    }
                    ```
                    """,
                Exercises = []
            };

            // ===== EJERCICIOS del capítulo 3 =====

            // Desafío de conceptos — operadores lógicos (lección 2)
            lessonCondiciones.Exercises.Add(new()
            {
                Order = 1,
                Type = ExerciseType.MultipleChoice,
                Title = "El inversor lógico",
                Statement = "Conceptos: operadores lógicos.",
                Question = "¿Cuál es el valor de F?\n\nint F;\nF = !(5 > 3);",
                Options = "1;0;5;Error de compilación",
                CorrectOptionIndex = 1,
                StarterCode = "",
                ExpectedOutput = "",
                RequiredTopicIds = [1]
            });

            // Desafío de código — el mayor de 3 números (lección 1)
            lessonIfElse.Exercises.Add(new()
            {
                Order = 1,
                Type = ExerciseType.CodeWriting,
                Title = "El mayor de 3 números",
                Statement = "Ingresar tres números enteros e indicar cuál es el mayor. Resolverlo con una condición compuesta (usando los operadores && del capítulo).",
                StarterCode = """
                    #include <stdio.h>

                    int main()
                    {
                        int A, B, C;

                        printf("Ingrese tres números: ");
                        scanf("%d %d %d", &A, &B, &C);

                        // Completar: mostrar el mayor de los tres
                        // Sugerencia: if (A > B && A > C) ...

                        return 0;
                    }
                    """,
                ExpectedOutput = "Ingrese tres números: \nEl mayor es 8\n",
                RequiredTopicIds = [1]
            });

            // Desafío de código — clasificar socios por edad (lección 3)
            lessonEscalonador.Exercises.Add(new()
            {
                Order = 1,
                Type = ExerciseType.CodeWriting,
                Title = "Clasificar socio por edad",
                Statement = "Ingresar la edad de un socio e imprimir su categoría: Infantil (menor de 14), Cadete (entre 14 y 20), Activo (entre 21 y 59), Senior (60 o más).",
                StarterCode = """
                    #include <stdio.h>

                    int main()
                    {
                        int EDAD;

                        printf("EDAD DEL SOCIO = ");
                        scanf("%d", &EDAD);

                        // Completar con el escalonador (else if)
                        // if (EDAD > 59) printf("SOCIO SENIOR\n");
                        // else if (EDAD > 20) printf("SOCIO ACTIVO\n");
                        // ...

                        return 0;
                    }
                    """,
                ExpectedOutput = "EDAD DEL SOCIO = \nSOCIO SENIOR\n",
                RequiredTopicIds = [1]
            });

            // Desafío de código — menú con switch (lección 4)
            lessonSwitch.Exercises.Add(new()
            {
                Order = 1,
                Type = ExerciseType.CodeWriting,
                Title = "Menú con switch",
                Statement = "Implementar un menú de 3 opciones con switch-case. El usuario ingresa 1, 2 o 3 y se informa la elección; cualquier otro valor muestra 'otra cosa'.",
                StarterCode = """
                    #include <stdio.h>

                    int main()
                    {
                        int SEL;

                        printf("1. OPCION 1\n");
                        printf("2. OPCION 2\n");
                        printf("3. OPCION 3\n");
                        printf("Ingrese su opción: ");
                        scanf("%d", &SEL);

                        // Completar con switch (SEL) { case 1: ... break; ... default: ... }

                        return 0;
                    }
                    """,
                ExpectedOutput = "1. OPCION 1\n2. OPCION 2\n3. OPCION 3\nIngrese su opción: \nUd. seleccionó OPCION 2\n",
                RequiredTopicIds = [1]
            });

            // Desafío de conceptos — la trampa del break (lección 4)
            lessonSwitch.Exercises.Add(new()
            {
                Order = 2,
                Type = ExerciseType.MultipleChoice,
                Title = "La trampa del break",
                Statement = "Conceptos: switch-case.",
                Question = "¿Qué imprime este código si SEL = 1?\n\nswitch (SEL) {\n    case 1: printf(\"A\");\n    case 2: printf(\"B\");\n             break;\n    default: printf(\"C\");\n}",
                Options = "A;AB;C;ABC",
                CorrectOptionIndex = 1,
                StarterCode = "",
                ExpectedOutput = "",
                RequiredTopicIds = [1]
            });

            // Ensamblamos el capítulo 3
            topicControlFlujo.Lessons.AddRange(
                [lessonIfElse, lessonCondiciones, lessonEscalonador, lessonSwitch]);
            courseC.Topics.Add(topicControlFlujo);

            context.SaveChanges();
            Console.WriteLine("✅ Seed incremental: capítulo 'Control de flujo' agregado (4 lecciones, 5 ejercicios)");
        }

        // ===== SEED INCREMENTAL: Capítulo 4 "Iteraciones" (idempotente) =====
        // Corre siempre; si el capítulo ya existe, no hace nada.
        if (!context.Topics.Any(t => t.Title == "Iteraciones"))
        {
            var courseC = context.Courses.Single(c => c.Name == "C");

            var topicIteraciones = new Topic
            {
                Order = 3,
                Title = "Iteraciones",
                Lessons = []
            };

            // ===== LECCIÓN 1: El lazo while =====
            var lessonWhile = new Lesson
            {
                Order = 1,
                Title = "El lazo while",
                ContentMarkdown = """
                    ## El lazo while

                    Una **iteración** es la ejecución repetida de una secuencia de sentencias mientras se cumpla una condición. El lazo `while` evalúa la condición **antes** de cada vuelta:

                    ```c
                    while (condición)
                        sentencia;
                    ```

                    ### ¡Cuidado con el lazo infinito!
                    La condición de permanencia debe **modificarse dentro del lazo** (dejar de cumplirse en algún momento). Si no, el lazo nunca termina y el programa se "cuelga".

                    ### Elemento centinela
                    Es un valor especial que marca el final de los datos. **No forma parte de los datos válidos**: es un "no-valor" que sirve para saber cuándo frenar el ingreso.

                    ### Ejemplo: sumar valores hasta un negativo

                    ```c
                    #include <stdio.h>

                    int main()
                    {
                        int ACUM = 0, VALOR;

                        printf("\nIngrese valores enteros.\n");
                        printf("\nFinaliza con un negativo\n\n");
                        scanf("%d", &VALOR);

                        while (VALOR >= 0)
                        {
                            ACUM = ACUM + VALOR;
                            scanf("%d", &VALOR);
                        }

                        printf("\nLa suma de los valores es %d\n\n", ACUM);
                        return 0;
                    }
                    ```

                    > **Estructura típica del bucle**: ingresar un dato → verificar en la condición → procesar → volver a ingresar dentro del lazo.
                    """,
                Exercises = []
            };

            // ===== LECCIÓN 2: do-while y condición ejecutiva =====
            var lessonDoWhile = new Lesson
            {
                Order = 2,
                Title = "do-while y condición ejecutiva",
                ContentMarkdown = """
                    ## El lazo do-while

                    El lazo `do-while` evalúa la condición **después** de ejecutar el cuerpo: **siempre se ejecuta al menos una vez**.

                    ```c
                    do
                    {
                        sentencia;
                    } while (condición);
                    ```

                    Es más compacto para el caso del centinela: ahorra el ingreso del dato previo al lazo.

                    ### Condición ejecutiva (efecto secundario)
                    La condición puede contener **sentencias ejecutables** cuyo resultado se evalúa. Se llama *efecto secundario* (side effect): la evaluación de la expresión genera ejecución de código y posibles modificaciones de variables.

                    ```c
                    while (getchar() != 'Q')
                        ;   // sentencia nula: el trabajo lo hace la condición
                    ```

                    ### Comparación vs asignación (¡trampa clásica!)
                    - `A == 2` → **comparación**: ¿A es igual a 2? (no modifica A)
                    - `A = 2` → **asignación**: A pasa a valer 2 (y el resultado siempre es "verdadero" si el valor es distinto de cero)
                    """,
                Exercises = []
            };

            // ===== LECCIÓN 3: El lazo for =====
            var lessonFor = new Lesson
            {
                Order = 3,
                Title = "El lazo for y la sentencia continue",
                ContentMarkdown = """
                    ## El lazo for

                    El `for` es un caso particular del `while`, pensado para **iteraciones con número fijo de vueltas**:

                    ```c
                    for (Inicialización; Condición; Incremento)
                        sentencia;
                    ```

                    - **Inicialización**: se ejecuta **una sola vez**, al inicio del lazo
                    - **Condición**: se evalúa al inicio de cada vuelta (la cantidad mínima de vueltas es cero)
                    - **Incremento**: se ejecuta al final de cada vuelta, justo antes de re-evaluar la condición

                    ### Ejemplo: promedio de 25 valores

                    ```c
                    #include <stdio.h>
                    #define N 25

                    int main()
                    {
                        int ACUM = 0, I, VALOR;
                        float PROM;

                        for (I = 0; I < N; I++)
                        {
                            scanf("%d", &VALOR);
                            ACUM = ACUM + VALOR;
                        }

                        PROM = (float)ACUM / N;   // casting: sin él, la división entera trunca
                        printf("El promedio es %.2f\n", PROM);
                        return 0;
                    }
                    ```

                    > **Convención del libro**: las variables enteras de uso general se nombran I, J, K...

                    ### La sentencia continue
                    Conduce el flujo directamente al **incremento** (pasa a la próxima vuelta), saltando el resto del cuerpo:

                    ```c
                    for (i = 0; i < 10; i++)
                    {
                        if (i == 2 || i == 6) continue;   // "saltea" el 2 y el 6
                        printf("\n%d", i);
                    }
                    ```

                    ### ¿for o while?
                    - `for`: cuando se conoce **previamente** el número de iteraciones
                    - `while`: cuando el número es **incierto**
                    - Es cuestión de estilo: cualquiera puede reemplazar al otro
                    """,
                Exercises = []
            };

            // ===== LECCIÓN 4: Desarrollo top-down y bottom-up =====
            var lessonDiseno = new Lesson
            {
                Order = 4,
                Title = "Desarrollo top-down y bottom-up",
                ContentMarkdown = """
                    ## Desarrollo top-down y bottom-up

                    Dos técnicas para construir programas, muy útiles en problemas de ingeniería.

                    ### Top-down (descendente)
                    Se empieza por la **descripción de mayor nivel** (el enunciado) y se la **divide en partes cada vez más simples** hasta que cada una pueda resolverse directamente. A esto se llama **refinamiento**.

                    Ejemplo: mostrar los números primos menores que 10.000
                    1. **Nivel I**: "imprimir los números primos menores que 10.000"
                    2. **Nivel II**: "recorrer NUM de 2 a 10.000; si es primo, mostrarlo"
                    3. **Nivel III**: "¿cómo sé si NUM es primo?" → bandera PRIMO + probar divisores DIV de 2 a NUM-1

                    ```c
                    for (NUM = 2; NUM < 10000; NUM++)
                    {
                        PRIMO = 1;
                        for (DIV = 2; DIV < NUM; DIV++)
                            if (!(NUM % DIV))
                                PRIMO = 0;
                        if (PRIMO)
                            printf("%8d", NUM);
                    }
                    ```

                    > El libro propone depuraciones: solo impares, cortar al encontrar divisor (`&& PRIMO`), y limitar DIV a `sqrt(NUM)` — el tiempo pasó de segundos a milisegundos.

                    ### Bottom-up (ascendente)
                    Se empieza por los **módulos más simples** y se agregan niveles de complejidad. Gran ventaja: **el código se prueba en cada paso**, con pocas líneas adicionales, y cada paso funciona antes de agregar el siguiente.

                    Ejemplo: densidad de primos por rango
                    1. ¿Es NUM divisible por DIV?
                    2. ¿Es N primo?
                    3. Contar primos entre INICIO y FIN
                    4. Repetir por rangos y promediar

                    > **Consejo del libro**: top-down genera módulos muy específicos; bottom-up genera módulos más generales y reutilizables. Ambos son herramientas útiles.
                    """,
                Exercises = []
            };

            // ===== EJERCICIOS del capítulo 4 =====

            // Desafío de código — problema propuesto 1 (lección 1)
            lessonWhile.Exercises.Add(new()
            {
                Order = 1,
                Type = ExerciseType.CodeWriting,
                Title = "Contar pares hasta un negativo",
                Statement = "Se ingresarán números enteros positivos. Determinar cuántos de ellos son pares. El ingreso finaliza con un número negativo (elemento centinela).",
                StarterCode = """
                    #include <stdio.h>

                    int main()
                    {
                        int VALOR, CONT = 0;

                        printf("Ingrese valores enteros (finaliza con negativo):\n");
                        scanf("%d", &VALOR);

                        // Completar: mientras VALOR >= 0,
                        // si es par (VALOR % 2 == 0) incrementar CONT,
                        // y volver a ingresar

                        printf("Cantidad de pares: %d\n", CONT);
                        return 0;
                    }
                    """,
                ExpectedOutput = "Ingrese valores enteros (finaliza con negativo):\nCantidad de pares: 2\n",
                RequiredTopicIds = [1, 2]
            });

            // Desafío de código — problema propuesto 5 (lección 2)
            lessonDoWhile.Exercises.Add(new()
            {
                Order = 1,
                Type = ExerciseType.CodeWriting,
                Title = "La clave con 3 intentos",
                Statement = "Permitir el ingreso de una clave numérica entera (23645). Solo se permiten 3 intentos; al tercer intento fallido mostrar una advertencia y terminar.",
                StarterCode = """
                    #include <stdio.h>

                    int main()
                    {
                        int CLAVE, INTENTOS = 0;

                        do
                        {
                            printf("Ingrese la clave: ");
                            scanf("%d", &CLAVE);
                            INTENTOS++;

                            if (CLAVE == 23645)
                            {
                                printf("CLAVE CORRECTA\n");
                                return 0;
                            }
                            printf("CLAVE INCORRECTA\n");

                        } while (INTENTOS < 3);

                        printf("ACCESO DENEGADO\n");
                        return 0;
                    }
                    """,
                ExpectedOutput = "Ingrese la clave: CLAVE INCORRECTA\nIngrese la clave: CLAVE INCORRECTA\nIngrese la clave: CLAVE INCORRECTA\nACCESO DENEGADO\n",
                RequiredTopicIds = [1, 2]
            });

            // Desafío de código — problema propuesto 6 (lección 3)
            lessonFor.Exercises.Add(new()
            {
                Order = 1,
                Type = ExerciseType.CodeWriting,
                Title = "Número perfecto",
                Statement = "Determinar si un número entero positivo ingresado es perfecto: un número es perfecto cuando es igual a la suma de todos sus divisores exactos, con excepción de él mismo. Ejemplo: 6 = 1 + 2 + 3.",
                StarterCode = """
                    #include <stdio.h>

                    int main()
                    {
                        int N, DIV, SUMA = 0;

                        printf("Ingrese un numero: ");
                        scanf("%d", &N);

                        // Completar: con un lazo for, sumar los divisores de N
                        // (DIV de 1 a N/2; si N % DIV == 0, sumar DIV)

                        if (SUMA == N)
                            printf("%d ES PERFECTO\n", N);
                        else
                            printf("%d NO ES PERFECTO\n", N);
                        return 0;
                    }
                    """,
                ExpectedOutput = "Ingrese un numero: \n6 ES PERFECTO\n",
                RequiredTopicIds = [1, 2]
            });

            // Desafío de código — problema propuesto 7 (lección 4)
            lessonDiseno.Exercises.Add(new()
            {
                Order = 1,
                Type = ExerciseType.CodeWriting,
                Title = "Los primeros 5 números perfectos",
                Statement = "Mostrar los primeros 5 números perfectos. Pista: los primeros son 6, 28, 496, 8128 y 33550336 — podés usar el desafío anterior dentro de un lazo que vaya probando números.",
                StarterCode = """
                    #include <stdio.h>

                    int main()
                    {
                        int ENCONTRADOS = 0, NUM = 2;

                        // Completar: mientras ENCONTRADOS < 5,
                        // determinar si NUM es perfecto (suma de divisores == NUM)
                        // y si lo es, mostrarlo e incrementar ENCONTRADOS
                        // luego probar con NUM++

                        return 0;
                    }
                    """,
                ExpectedOutput = "6\n28\n496\n8128\n33550336\n",
                RequiredTopicIds = [1, 2]
            });

            // Desafío de conceptos — la sentencia continue (lección 3)
            lessonFor.Exercises.Add(new()
            {
                Order = 2,
                Type = ExerciseType.MultipleChoice,
                Title = "La sentencia continue",
                Statement = "Conceptos: lazos y continue.",
                Question = "¿Qué imprime este código?\n\nfor (i = 0; i < 10; i++) {\n    if (i == 2 || i == 6) continue;\n    printf(\"%d\", i);\n}",
                Options = "0123456789;01345789;02468;13579",
                CorrectOptionIndex = 1,
                StarterCode = "",
                ExpectedOutput = "",
                RequiredTopicIds = [1, 2]
            });

            // Desafío de conceptos — comparación vs asignación (lección 2)
            lessonDoWhile.Exercises.Add(new()
            {
                Order = 2,
                Type = ExerciseType.MultipleChoice,
                Title = "Comparación vs asignación",
                Statement = "Conceptos: condiciones.",
                Question = "¿Qué hace este código?\n\nint F = 0;\nif (F = 2)\n    printf(\"VERDADERO\");",
                Options = "Imprime VERDADERO, porque F = 2 asigna el valor 2 (distinto de cero es verdadero);No imprime nada, porque F valía 0;Imprime VERDADERO porque 2 es igual a F;Error de compilación",
                CorrectOptionIndex = 0,
                StarterCode = "",
                ExpectedOutput = "",
                RequiredTopicIds = [1, 2]
            });

            // Ensamblamos el capítulo 4
            topicIteraciones.Lessons.AddRange(
                [lessonWhile, lessonDoWhile, lessonFor, lessonDiseno]);
            courseC.Topics.Add(topicIteraciones);

            context.SaveChanges();
            Console.WriteLine("✅ Seed incremental: capítulo 'Iteraciones' agregado (4 lecciones, 6 ejercicios)");
        }
    }
}
