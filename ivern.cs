namespace Prog { 
public class Program
{

        public class BinaryTree<T> where T : IComparable<T>
        {
            public BinaryTreeNode<T> Root { get; set; }

            public void Add(T value)
            {
                if (Root == null)
                {
                    Root = new BinaryTreeNode<T>(value);
                    return;
                }

                AddNode(value, Root);
            }

            private void AddNode(T value, BinaryTreeNode<T> current)
            {
                if (value.CompareTo(current.Value) < 0)
                {
                    if (current.LeftChild == null)
                    {
                        current.LeftChild = new BinaryTreeNode<T>(value);
                    }
                    else
                    {
                        AddNode(value, current.LeftChild);
                    }
                }
                else
                {
                    if (current.RightChild == null)
                    {
                        current.RightChild = new BinaryTreeNode<T>(value);
                    }
                    else
                    {
                        AddNode(value, current.RightChild);
                    }
                }
            }

            public void Remove(T value)
            {
                if (Root == null)
                {
                    throw new InvalidOperationException("Tree is empty.");
                }

                RemoveNode(value, Root, null);
            }

            private BinaryTreeNode<T> RemoveNode(T value, BinaryTreeNode<T> current, BinaryTreeNode<T> parent)
            {
                if (current == null)
                {
                    return null;
                }

                if (value.CompareTo(current.Value) < 0)
                {
                    current.LeftChild = RemoveNode(value, current.LeftChild, current);
                }
                else if (value.CompareTo(current.Value) > 0)
                {
                    current.RightChild = RemoveNode(value, current.RightChild, current);
                }
                else
                {
                    if (current.LeftChild == null)
                    {
                        if (parent == null)
                        {
                            Root = current.RightChild;
                        }
                        else
                        {
                            if (parent.LeftChild == current)
                            {
                                parent.LeftChild = current.RightChild;
                            }
                            else
                            {
                                parent.RightChild = current.RightChild;
                            }
                        }
                    }
                    else if (current.RightChild == null)
                    {
                        if (parent == null)
                        {
                            Root = current.LeftChild;
                        }
                        else
                        {
                            if (parent.LeftChild == current)
                            {
                                parent.LeftChild = current.LeftChild;
                            }
                            else
                            {
                                parent.RightChild = current.LeftChild;
                            }
                        }
                    }
                    else
                    {
                        BinaryTreeNode<T> successor = GetSuccessor(current.RightChild);
                        current.Value = successor.Value;
                        current.RightChild = RemoveNode(successor.Value, current.RightChild, current);
                    }
                }

                return current;
            }

            private BinaryTreeNode<T> GetSuccessor(BinaryTreeNode<T> node)
            {
                if (node.LeftChild == null)
                {
                    return node;
                }
                else
                {
                    return GetSuccessor(node.LeftChild);
                }
            }

            public void PrintBottomUp()
            {
                PrintBottomUp(Root);
            }

            private void PrintBottomUp(BinaryTreeNode<T> node)
            {
                if (node != null)
                {
                    PrintBottomUp(node.LeftChild);
                    PrintBottomUp(node.RightChild);
                    Console.WriteLine(node.Value);
                }
            }

            public void PrintTopDown()
            {
                PrintTopDown(Root);
            }

            private void PrintTopDown(BinaryTreeNode<T> node)
            {
                if (node != null)
                {
                    Console.WriteLine(node.Value);
                    PrintTopDown(node.LeftChild);
                    PrintTopDown(node.RightChild);
                }
            }

            public bool Contains(T value)
            {
                if (Root == null)
                {
                    return false;
                }

                return Contains(value, Root);
            }

            private bool Contains(T value, BinaryTreeNode<T> current)
            {
                if (current == null)
                {
                    return false;
                }

                if (value.CompareTo(current.Value) == 0)
                {
                    return true;
                }
                else if (value.CompareTo(current.Value) < 0)
                {
                    return Contains(value, current.LeftChild);
                }
                else
                {
                    return Contains(value, current.RightChild);
                }
            }
        }

        public class BinaryTreeNode<T>
        {
            public T Value { get; set; }
            public BinaryTreeNode<T> LeftChild { get; set; }
            public BinaryTreeNode<T> RightChild { get; set; }

            public BinaryTreeNode(T value)
            {
                Value = value;
            }
        }
        public static void Main(string[] args)
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            Console.WriteLine("Вот ваше базовое дерево:");
            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(4);
            tree.Add(5);
            tree.Add(6);
            tree.PrintTopDown();
            while (true)
            {
                Console.WriteLine("Введите команду (добавить, удалить, найти, вывести, выйти):");
                string command = Console.ReadLine();
                
                switch (command)
                {
                    case "добавить":
                        Console.WriteLine("Введите значение для добавления:");
                        int value = int.Parse(Console.ReadLine());
                        tree.Add(value);
                        Console.WriteLine("Значение " + value+" добавленно");
                        break;

                    case "удалить":
                        Console.WriteLine("Введите значение для удаления:");
                        int valueToRemove = int.Parse(Console.ReadLine());
                        tree.Remove(valueToRemove);
                        Console.WriteLine("Значение " + valueToRemove + " удалено");
                        break;

                    case "найти":
                        Console.WriteLine("Введите значение для поиска:");
                        int valueToFind = int.Parse(Console.ReadLine());
                        bool found = tree.Contains(valueToFind);
                        Console.WriteLine($"Значение {valueToFind} {(found ? "найдено" : "не найдено")}");
                        break;

                    case "вывести":
                        Console.WriteLine("Вывод дерева снизу вверх:");
                        tree.PrintBottomUp();
                        Console.WriteLine("Вывод дерева сверху вниз:");
                        tree.PrintTopDown();
                        break;

                    case "выйти":
                        return;

                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
        }
    
}
}