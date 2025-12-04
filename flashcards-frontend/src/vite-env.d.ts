interface ImportMetaEnv {
  readonly VITE_API_BASE_URL: string;
  // lägg till fler VITE_ variabler här om du behöver
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}