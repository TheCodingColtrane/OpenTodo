import { Routes, Route } from 'react-router-dom';
import Board from './components/pages/board'
import SignUp from './components/pages/user/SignUp';
import SignIn from './components/pages/SignIn';
import Choose from './components/pages/board/choose';

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<Board />} />
      <Route path="/board/:id?" element={<Board />} />
      <Route path="/signup" element={<SignUp />} />
      <Route path="/signin" element={<SignIn />} />
      <Route path="/board/choose" element={<Choose />} />
    </Routes>
  );
};

export default AppRoutes;