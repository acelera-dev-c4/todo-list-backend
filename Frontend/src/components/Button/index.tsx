interface IButton {
  label: string;
  disabled?: boolean;
  onClick: () => void;
}

export default function Button({ label, disabled, onClick }: IButton) {
  return (
    <button
      type="button"
      onClick={onClick}
      disabled={disabled}
      className="w-full h-10 px-4 bg-cyan-600 rounded-md text-white disabled:opacity-45 disabled:cursor-not-allowed"
    >
      {label}
    </button>
  );
}
