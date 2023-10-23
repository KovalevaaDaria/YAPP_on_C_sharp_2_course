# Лабораторная работа №5 ООП на C#

## Теоретическая часть:

### Перегрузка операторов в C#:
Наряду с методами мы можем также перегружать операторы. Например, пусть у нас есть следующий класс Counter:

```
class Counter
{
    public int Value { get; set; }
}
```

Данный класс представляет некоторый счетчик, значение которого хранится в свойстве Value.
И допустим, у нас есть два объекта класса Counter - два счетчика, которые мы хотим сравнивать или складывать на основании их свойства Value, используя стандартные операции сравнения и сложения:

```
Counter c1 = new Counter { Value = 23  };
Counter c2 = new Counter { Value = 45 };
 
bool result = c1 > c2;
Counter c3 = c1 + c2;
```

Но на данный момент ни операция сравнения, ни операция сложения для объектов Counter не доступны. Эти операции могут использоваться для ряда примитивных типов. Например, по умолчанию мы можем складывать числовые значения, но как складывать объекты комплексных типов - классов и структур компилятор не знает. И для этого нам надо выполнить перегрузку нужных нам операторов.
Перегрузка операторов заключается в определении в классе, для объектов которого мы хотим определить оператор, специального метода:
public static возвращаемый_тип operator оператор(параметры)
{  }

Этот метод должен иметь модификаторы public static, так как перегружаемый оператор будет использоваться для всех объектов данного класса. Далее идет название возвращаемого типа. Возвращаемый тип представляет тот тип, объекты которого мы хотим получить. К примеру, в результате сложения двух объектов Counter мы ожидаем получить новый объект Counter. А в результате сравнения двух мы хотим получить объект типа bool, который указывает истинно ли условное выражение или ложно. Но в зависимости от задачи возвращаемые типы могут быть любыми.
Затем вместо названия метода идет ключевое слово operator и собственно сам оператор. И далее в скобках перечисляются параметры. Бинарные операторы принимают два параметра, унарные - один параметр. И в любом случае один из параметров должен представлять тот тип - класс или структуру, в котором определяется оператор.
Например, перегрузим ряд операторов для класса Counter:

```
class Counter
{
    public int Value { get; set; }
         
    public static Counter operator +(Counter c1, Counter c2)
    {
        return new Counter { Value = c1.Value + c2.Value };
    }
    public static bool operator >(Counter c1, Counter c2)
    {
        return c1.Value > c2.Value;
    }
    public static bool operator <(Counter c1, Counter c2)
    {
        return c1.Value < c2.Value;
    }
}
```

Поскольку все перегруженные операторы - бинарные - то есть проводятся над двумя объектами, то для каждой перегрузки предусмотрено по два параметра.
Так как в случае с операцией сложения мы хотим сложить два объекта класса Counter, то оператор принимает два объекта этого класса. И так как мы хотим в результате сложения получить новый объект Counter, то данный класс также используется в качестве возвращаемого типа. Все действия этого оператора сводятся к созданию, нового объекта, свойство Value которого объединяет значения свойства Value обоих параметров:

```
public static Counter operator +(Counter c1, Counter c2)
{
    return new Counter { Value = c1.Value + c2.Value };
}
```

Также переопределены две операции сравнения. Если мы переопределяем одну из этих операций сравнения, то мы также должны переопределить вторую из этих операций. Сами операторы сравнения сравнивают значения свойств Value и в зависимости от результата сравнения возвращают либо true, либо false.
Теперь используем перегруженные операторы в программе:

```
static void Mclass Counter
{
    public int Value { get; set; }
         
    public static Counter operator +(Counter c1, Counter c2)
    {
        return new Counter { Value = c1.Value + c2.Value };
    }
    public static bool operator >(Counter c1, Counter c2)
    {
        return c1.Value > c2.Value;
    }
    public static bool operator <(Counter c1, Counter c2)
    {
        return c1.Value < c2.Value;
    }
}ain(string[] args)
{
    Counter c1 = new Counter { Value = 23 };
    Counter c2 = new Counter { Value = 45 };
    bool result = c1 > c2;
    Console.WriteLine(result); // false
 
    Counter c3 = c1 + c2;
    Console.WriteLine(c3.Value);  // 23 + 45 = 68
     
    Console.ReadKey();
}
```

Стоит отметить, что так как по сути определение оператора представляет собой метод, то этот метод мы также можем перегрузить, то есть создать для него еще одну версию. Например, добавим в класс Counter еще один оператор:

```
public static int operator +(Counter c1, int val)
{
    return c1.Value + val;
}
```

Данный метод складывает значение свойства Value и некоторое число, возвращая их сумму. И также мы можем применить этот оператор:

```
Counter c1 = new Counter { Value = 23 };
int d = c1 + 27; // 50
Console.WriteLine(d);
```

Следует учитывать, что при перегрузке не должны изменяться те объекты, которые передаются в оператор через параметры. Например, мы можем определить для класса Counter оператор инкремента:

```
public static Counter operator ++(Counter c1)
{
    c1.Value += 10;
    return c1;
}
```

Поскольку оператор унарный, он принимает только один параметр - объект того класса, в котором данный оператор определен. Но это неправильное определение инкремента, так как оператор не должен менять значения своих параметров.
И более корректная перегрузка оператора инкремента будет выглядеть так:

```
public static Counter operator ++(Counter c1)
{
    return new Counter { Value = c1.Value + 10 };
}
```

То есть возвращается новый объект, который содержит в свойстве Value инкрементированное значение.
При этом нам не надо определять отдельно операторы для префиксного и для постфиксного инкремента (а также декремента), так как одна реализация будет работать в обоих случаях.
Например, используем операцию префиксного инкремента:

```
Counter counter = new Counter() { Value = 10 };
Console.WriteLine($"{counter.Value}");  	// 10
Console.WriteLine($"{(++counter).Value}");  // 20
Console.WriteLine($"{counter.Value}");  	// 20

Консольный вывод:
10
20
20
```
```
Теперь используем постфиксный инкремент:
Counter counter = new Counter() { Value = 10 };
Console.WriteLine($"{counter.Value}");  	// 10
Console.WriteLine($"{(counter++).Value}");  // 10
Console.WriteLine($"{counter.Value}");  	// 20

Консольный вывод:
10
10
20
```

Также стоит отметить, что мы можем переопределить операторы true и false. Например, определим их в классе Counter:

```
class Counter
{
    public int Value { get; set; }
     
    public static bool operator true(Counter c1)
    {
        return c1.Value != 0;
    }
    public static bool operator false(Counter c1)
    {
        return c1.Value == 0;
    }
     
    // остальное содержимое класса
}
```

Эти операторы перегружаются, когда мы хотим использовать объект типа в качестве условия. 
Например:

```
Counter counter = new Counter() { Value = 0 };
if (counter)
    Console.WriteLine(true);
else
    Console.WriteLine(false);
```

При перегрузке операторов надо учитывать, что не все операторы можно перегрузить. В частности, мы можем перегрузить следующие операторы:
унарные операторы +, -, !, ~, ++, --
бинарные операторы +, -, *, /, %
операции сравнения ==, !=, <, >, <=, >=
логические операторы &&, ||
И есть ряд операторов, которые нельзя перегрузить, например, операцию равенства = или тернарный оператор ?:, а также ряд других.
Полный список перегружаемых операторов можно найти в документации msdn.
При перегрузке операторов также следует помнить, что мы не можем изменить приоритет оператора или его ассоциативность, мы не можем создать новый оператор или изменить логику операторов в типах, который есть по умолчанию в .NET.
Индексаторы
Перегрузка оператора [] называется созданием индексатора и осуществляется по своим правилам. 
Пример:

```
using System;

class SampleCollection<T>
{
   // Declare an array to store the data elements.
   private T[] arr = new T[100];

   // Define the indexer to allow client code to use [] notation.
   public T this[int i]
   {
      get { return arr[i]; }
      set { arr[i] = value; }
   }
}

class Program
{
   static void Main()
   {
      var stringCollection = new SampleCollection<string>();
      stringCollection[0] = "Hello, World";
      Console.WriteLine(stringCollection[0]);
   }
}
```

Более подробную информацию об индексаторах можно узнать на msdn.
Задание на лабораторную работу
Реализация математического вектора
Необходимо в отдельной сборке, то есть в отдельном проекте типа ClassLibrary (дать ему название LinearAlgebra), написать класс MathVector, представляющий собой реализацию математического вектора. Класс должен реализовывать представленный ниже интерфейс IMathVector, являющийся наследником интерфейса IEnumerable:

```
public interface IMathVector : IEnumerable
{
	/// <summary>
      /// Получить размерность вектора (количество координат).ф
      /// </summary>
	int Dimensions { get; }

	/// <summary>
      /// Индексатор для доступа к элементам вектора. Нумерация с нуля.
      /// </summary>
	double this[int i] { get; set; }

      /// <summary>Рассчитать длину (модуль) вектора.</summary>
	double Length { get; } 

	/// <summary>Покомпонентное сложение с числом.</summary>
	IMathVector SumNumber(double number);

	/// <summary>Покомпонентное умножение на число.</summary>
	IMathVector MultiplyNumber(double number);

	/// <summary>Сложение с другим вектором.</summary>
	IMathVector Sum(IMathVector vector);

	/// <summary>Покомпонентное умножение с другим вектором.</summary>
	IMathVector Multiply(IMathVector vector);

	/// <summary>Скалярное умножение на другой вектор.</summary>
	double ScalarMultiply(IMathVector vector);

	/// <summary>
      /// Вычислить Евклидово расстояние до другого вектора.
      /// </summary>
	double CalcDistance(IMathVector vector);
}
```

При этом, методы, возвращающие IMathVector, должны создавать новый объект, а не модифицировать текущий, то есть быть иммутабельными.
Кроме этого, класс должен иметь перегрузки следующих операторов:
1) + покомпонентное сложение с числом или другим вектором
2) - покомпонентное вычитание с числом или другим вектором
3) * покомпонентное умножение с числом или другим вектором
4) / покомпонентное деление с числом или другим вектором
5) % скалярное умножение двух векторов
Для реализации перегруженных операторов необходимо использовать методы из интерфейса IMathVector.

Для всех некорректных операций должны быть предусмотрены исключения.

Демонстрационное консольное приложение
В том же решении (solution), где находится проект LinearAlgebra типа ClassLibrary создать новый проект по типу ConsoleApplication и назвать его VectorDemo, где на простых демонстрационных данных продемонстрировать работоспособность каждого из реализованных методов класса MathVector. При этом объекты векторов должны быть объявлены по типу интерфейса IMathVector.

Общие требования к лабораторным работам:
1) Следовать принципам KISS, DRY, YAGNI и т.п.
2) Следовать принципам ООП
3) Код приложения должен быть разделен на “слои”/компоненты, как минимум:
компонент логики - отвечает за логику приложения, содержит доменные модели, классы, позволяющие с ними взаимодействовать и реализующие основную задачу, поставленную перед приложением. Должен быть независим от конкретной реализации других компонентов! Не должен содержать логику ввода-вывода.
компоненты ввода/вывода - отвечают за ввод данных пользователем, чтение и запись данных в файл и т. п. не должен содержать логики обработки данных, только чтение, парсинг, форматированный вывод.
4) Приложение должно корректно обрабатывать (выводить понятную пользователю ошибку , а не падать) ошибочный ввод пользователя, ситуации отсутствия необходимого файла, некорректных данных и т.п.
5) Код должен соответствовать code-style соответствующего языка: для C# спрашивайте у вашего семинариста-лаборанта
