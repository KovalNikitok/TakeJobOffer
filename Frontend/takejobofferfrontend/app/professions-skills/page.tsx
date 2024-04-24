"use client";

import Title from "antd/es/typography/Title";
import Link from "antd/es/typography/Link";
import { Professions } from "../components/ProfessionsSkills";
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
      // if(professions.length === 0)
      //   professions.push({name: "C#", description: "C# developer", id: "0b31ce20-4b95-4411-ba37-617cce14fb84"} as Profession)
      setLoading(false);
      setProfessions(professions);
      };

    getProfessions();
  }, []);

  const openDetailed = async (profession: Profession) => {
  };

  return (
    <div>
      {loading ? (
        <Title>Loading...</Title>
      ) : (
        <Professions
          professions={professions}
          handleDetailed={openDetailed}
        />
      )}
    </div>
  );
}
