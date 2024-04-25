"use client";

import Title from "antd/es/typography/Title";
import { ProfessionsSkills } from "../components/ProfessionsSkills";
import {
  getAllProfessions,
} from "../services/professions";
import { useEffect, useState } from "react";

export default function ProfessionsPage() {
  const [professions, setProfessions] = useState<Profession[]>([]);
  const [loading, setLoading] = useState(true);

  const [values, setValues] = useState<Profession>({
    name: "",
    description: "",
  } as Profession);

  const defaultValues = {
    name: "",
    description: "",
  } as Profession;

  useEffect(() => {
    const getProfessions = async () => {
        const professions = await getAllProfessions();

        setLoading(false);
        setProfessions(professions);
      };

    getProfessions();
  }, []);

  return (
    <div>
      {loading ? (
        <Title>Loading...</Title>
      ) : (
        <ProfessionsSkills
          professions={professions}
        />
      )}
    </div>
  );
}
