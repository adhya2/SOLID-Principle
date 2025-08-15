#include <iostream>
#include <vector>

using namespace std;
class Product {
public:
    string name;
    double price;

    Product(string name, double price) {
        this->name = name;
        this->price = price;
    }
};

class ShoppingCart {
private:
    vector<Product*> products; 

public:
    void addProduct(Product* p) { 
        products.push_back(p);
    }

    const vector<Product*>& getProducts() { 
        return products;
    } 


    double calculateTotal() {
        double total = 0;
        for (auto p : products) {
            total += p->price;
        }
        return total;
    }
};


class ShoppingCartPrinter {
private:
    ShoppingCart* cart; 

public:
    ShoppingCartPrinter(ShoppingCart* cart) { 
        this->cart = cart; 
    }

    void printInvoice() {
        cout << "Shopping Cart Invoice:\n";
        for (auto p : cart->getProducts()) {
            cout << p->name << " - Rs " << p->price << endl;
        }
        cout << "Total: Rs " << cart->calculateTotal() << endl;
    }
};


class Persistence {
private:
    ShoppingCart* cart;

public:
    virtual void save(ShoppingCart* cart) = 0; // Pure virtual function
};

class SQLPersistence : public Persistence {
public:
    void save(ShoppingCart* cart) override {
        cout << "Saving shopping cart to SQL DB..." << endl;
    }
};

class MongoPersistence : public Persistence {
public:
    void save(ShoppingCart* cart) override {
        cout << "Saving shopping cart to MongoDB..." << endl;
    }
};

class FilePersistence : public Persistence {
public:
    void save(ShoppingCart* cart) override {
        cout << "Saving shopping cart to a file..." << endl;
    }
};

int main() {
    ShoppingCart* cart = new ShoppingCart();
    Product* p1 = new Product("Laptop", 50000);
    Product* p2 = new Product("Mouse", 2000);
    cart->addProduct(p1);
    cart->addProduct(p2);

    ShoppingCartPrinter* printer = new ShoppingCartPrinter(cart);
    printer->printInvoice();

    Persistence* db = new SQLPersistence();
    Persistence* mongo = new MongoPersistence();
    Persistence* file = new FilePersistence();

    db->save(cart);   
    mongo->save(cart); 
    file->save(cart);  

    return 0;
}