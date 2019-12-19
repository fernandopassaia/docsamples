import { createAppContainer, createSwitchNavigator } from 'react-navigation';
import { Login } from './pages/Login';
import { Book } from './pages/Book';
import { Pages } from './pages/Pages';

const routes = createAppContainer(
    createSwitchNavigator()
)