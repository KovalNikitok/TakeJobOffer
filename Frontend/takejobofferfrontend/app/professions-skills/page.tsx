"use client";

import Title from "antd/es/typography/Title";
import { ProfessionsSkills } from "../components/ProfessionsSkills";
import {
  getAllProfessionsWithSlug,
} from "../services/professions";
import { useEffect, useState } from "react";

export default function ProfessionsPage() {
  const [professions, setProfessions] = useState<ProfessionWithSlug[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getProfessions = async () => {
        const professions = await getAllProfessionsWithSlug();

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
        <ProfessionsSkills professions={professions} />
      )}
    </div>
  );
}


