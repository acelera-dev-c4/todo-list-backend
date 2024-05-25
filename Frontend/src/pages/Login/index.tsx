import { useState } from "react";
import Button from "../../components/Button";
import axios from "axios";

export default function Login() {
  const [loading, setLoading] = useState(false);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleButtonClick = async () => {
    try {
      setLoading(true);

      const params = {
        email,
        password,
      };

      const { data } = await axios.post(
        "https://aceleradev.sharebook.com.br/Auth",
        params,
        {
          headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
          },
        }
      );

      setLoading(false);

      console.log("data", data);
    } catch (error) {
      console.log("error", error);
    }
  };

  return (
    <div className="w-full h-full flex items-center justify-center bg-gray-100 p-5">
      <div className="w-full min-w-[400px] md:w-auto flex flex-col items-center p-5 gap-5 shadow-xl border border-zinc-200 bg-zinc-50 rounded-md">
        <h1 className="text-black text-2xl font-bold">Login</h1>

        <form className="w-full flex flex-col gap-5 items-center">
          <div className="w-full flex flex-col gap-1">
            <label className="text-base font-semibold">E-mail</label>

            <input
              type="email"
              placeholder="Digite seu e-mail"
              className="w-full h-10 px-4 rounded-md border border-zinc-300 outline-none"
              value={email}
              onChange={({ target }) => setEmail(target.value)}
            />
          </div>

          <div className="w-full flex flex-col gap-1">
            <label className="text-base font-semibold">Senha</label>

            <input
              type="password"
              placeholder="Digite sua senha"
              className="w-full h-10 px-4 rounded-md border border-zinc-300 outline-none"
              value={password}
              onChange={({ target }) => setPassword(target.value)}
            />
          </div>

          <Button
            label="Entrar"
            onClick={handleButtonClick}
            disabled={loading}
          />
        </form>
      </div>
    </div>
  );
}
