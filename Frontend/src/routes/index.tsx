import {
  BrowserRouter,
  Route,
  Routes as RoutesContainer,
} from "react-router-dom";
import Login from "../pages/Login";
import Home from "../pages/Home";

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
