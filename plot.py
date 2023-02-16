import re
import matplotlib.pyplot as plt
import numpy as np
import sys

replacements = {
    'sin' : 'np.sin',
    'cos' : 'np.cos',
    'tan':"np.tan",
    'exp': 'np.exp',
    'sqrt': 'np.sqrt',
    '^': '**',
    "pi": 'np.pi',
}

allowed_words = [
    'x',
    'sin',
    'cos',
    "tan",
    'sqrt',
    'exp',
    "pi"
]

def string2func(string):
    ''' evaluates the string and returns a function of x '''
    # find all words and check if all are allowed:
    for word in re.findall('[a-zA-Z_]+', string):
        if word not in allowed_words:
            raise ValueError(
                '"{}" is forbidden to use in math expression'.format(word)
            )

    for old, new in replacements.items():
        string = string.replace(old, new)

    def func(x):
        return eval(string)

    return func


if __name__ == '__main__':
    func=None
    func_str=None
    if len(sys.argv)>1:
        func_str=sys.argv[1]
        func=string2func(sys.argv[1])
    else:
        func_str=input('enter function: f(x) = ')
        func = string2func(func_str)
    # a = float(input('enter lower limit: '))
    # b = float(input('enter upper limit: '))
    plt.title("f(x):"+func_str)
    a=-1000.0
    b=1000.0
    x = np.linspace(a, b, 2000)
    if "x" not in func_str:#tabe sabet
        plt.plot(x, np.full(x.shape, func(x)))
    else:
        plt.plot(x, func(x))
    plt.xlim(a, b)
    plt.show()