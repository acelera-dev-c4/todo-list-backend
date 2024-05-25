import {
  BrowserRouter,
  Route,
  Routes as RoutesContainer,
} from "react-router-dom";
import {
  Login,
  Home
} from '../pages';

export default function Routes() {
  return (
    <BrowserRouter>
      <RoutesContainer>
        <Route path="/login" element={<Login />} />
        <Route path="/" element={<Home />} />
      </RoutesContainer>
    </BrowserRouter>
  );
}
